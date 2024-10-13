using C.Common.GlobalResponses;
using FluentValidation;
using MediatR;
using System.Globalization;

namespace E.Application.PipelineBehaviours;
public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    where TResponse : ResponseModel, new()  // ResponseModel türünden olmasını garanti ediyoruz
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    static ValidationPipelineBehaviour()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("az");
    }

    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> compositeValidator)
    {
        _validators = compositeValidator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        // Tüm validatorları çağırıyoruz ve asenkron olarak çalıştırıyoruz
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Geçersiz olan sonuçları toplayalım
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Any())
        {
            // Hataları ResponseModel'in `Errors` listesine ekliyoruz
            var response = new TResponse
            {
                IsSuccess = false,
                Errors = failures.Select(f => f.ErrorMessage).ToList() // Hataların mesajlarını topluyoruz
            };

            return response;
        }

        // Eğer hata yoksa işlemi devam ettiriyoruz
        return await next();
    }
}
