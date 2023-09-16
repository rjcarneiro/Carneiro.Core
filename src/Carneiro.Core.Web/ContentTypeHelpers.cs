namespace Carneiro.Core.Web;

/// <summary>
/// The content type helpers.
/// </summary>
public static class ContentTypeHelpers
{
    /// <summary>
    /// All image content types
    /// </summary>
    /// <remarks>Reference: https://www.iana.org/assignments/media-types/media-types.xhtml#image</remarks>
    public static readonly string[] AllImageContentTypes =
    {
        "image/aces", "image/avci", "image/avcs", "image/avif", "image/bmp", "image/cgm", "image/dicom-rle",
        "image/emf", "image/example", "image/fits", "image/g3fax", "image/gif", "image/heic",
        "image/heic-sequence", "image/heif", "image/heif-sequence", "image/hej2k", "image/hsj2", "image/ief",
        "image/jls", "image/jp2", "image/jpeg", "image/jph", "image/jphc", "image/jpm", "image/jpx",
        "image/jxr", "image/jxrA", "image/jxrS", "image/jxs", "image/jxsc", "image/jxsi", "image/jxss",
        "image/ktx", "image/ktx2", "image/naplps", "image/png", "image/prs.btif", "image/prs.pti",
        "image/pwg-raster", "image/svg+xml", "image/t38", "image/tiff", "image/tiff-fx",
        "image/vnd.adobe.photoshop", "image/vnd.airzip.accelerator.azv", "image/vnd.cns.inf2",
        "image/vnd.dece.graphic", "image/vnd.djvu", "image/vnd.dwg", "image/vnd.dxf", "image/vnd.dvb.subtitle",
        "image/vnd.fastbidsheet", "image/vnd.fpx", "image/vnd.fst", "image/vnd.fujixerox.edmics-mmr",
        "image/vnd.fujixerox.edmics-rlc", "image/vnd.globalgraphics.pgb", "image/vnd.microsoft.icon",
        "image/vnd.mix", "image/vnd.ms-modi", "image/vnd.mozilla.apng", "image/vnd.net-fpx",
        "image/vnd.pco.b16", "image/vnd.radiance", "image/vnd.sealed.png", "image/vnd.sealedmedia.softseal.gif",
        "image/vnd.sealedmedia.softseal.jpg", "image/vnd.svf", "image/vnd.tencent.tap",
        "image/vnd.valve.source.texture", "image/vnd.wap.wbmp", "image/vnd.xiff", "image/vnd.zbrush.pcx",
        "image/wmf", "image/emf", "image/wmf"
    };

    /// <summary>
    /// The available image content types.
    /// </summary>
    public static readonly string[] AvailableImageContentTypes =
    {
        "image/jpeg",
        "image/bmp",
        "image/heic",
        "image/svg+xml",
        "image/png",
        "image/tiff"
    };
}