
namespace XFEMediaPickerSample.Utils
{
    public interface IImageCompressionService
    {
        byte[] CompressImage(byte[] imageData, string destinationPath, int compressionPercentage);
    }
}
