namespace MatthewSandPit.API.Managers.Interfaces
{
    public interface IPDFManager
    {
       byte[] MergePDFs(IEnumerable<Stream> files);

    }
}
