using System.Reflection;

namespace PaymentService.CibPayIntegration.Certificate;

public class CertificatePath
{
    public string CurrentPath { get; }

    public CertificatePath()
    {
        var assemblyFolder = Assembly.GetExecutingAssembly().Location;
        var certificatePath = Path.Combine(Path.GetDirectoryName(assemblyFolder)!, "CibPayIntegration/Certificate/taxiapp.p12");
        CurrentPath = certificatePath;
    }
}
