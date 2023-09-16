namespace Carneiro.Tests.Core.Extensions;

[TestFixture]
public class StringExtensionsTests
{
    [Test]
    [TestCase("Multiple Spaces")]
    [TestCase("Multiple  Spaces")]
    [TestCase("Multiple   Spaces")]
    [TestCase("Multiple   Spaces")]
    [TestCase("Multiple    Spaces")]
    public void When_MultipleSpaces_MustOnlyHave_OneSpace(string str)
    {
        var newString = str.RemoveDuplicatedWhiteSpaces();
        Assert.That(newString, Is.Not.Null.Or.Empty);

        var numberOfSpaces = newString.Count(t => t == ' ');
        Assert.That(numberOfSpaces, Is.EqualTo(1));
    }

    [Test]
    public void When_EmptyString_ShouldBeEmpty()
    {
        var myStr = string.Empty;
        var newStr = myStr.RemoveDuplicatedWhiteSpaces();
        Assert.That(newStr, Is.EqualTo(string.Empty));
    }

    [Test]
    public void When_Null_ThrowException()
    {
        string myStr = null;
        Assert.Throws<ArgumentNullException>(() => myStr.RemoveDuplicatedWhiteSpaces());
    }

    [Test]
    [TestCase("Savida Ribbed Pom Hat", "savida-ribbed-pom-hat")]
    [TestCase("Acer 311 11 Inch Celeron 4GB 32GB Chromebook - Silver", "acer-311-11-inch-celeron-4gb-32gb-chromebook")]
    [TestCase("Air Wick Scented Oil Refill Mediterranean Sun 19Ml", "air-wick-scented-oil-refill-mediterranean-sun")]
    [TestCase("Salted Caramel Ice Cream 3X90 Ml", "salted-caramel-ice-cream-3x90-ml")]
    [TestCase("Ryobi OLT1832 ONE+ Grass Trimmer Bare Tool - 18V", "ryobi-olt1832-one-grass-trimmer-bare-tool-1")]
    [TestCase("Snowflake Tinsel", "snowflake-tinsel")]
    [TestCase("Quick Dry Half Zip Run Top", "quick-dry-half-zip-run-top")]
    [TestCase("LEGO DUPLO Mickey's Boat", "lego-duplo-mickeys-boat")]
    [TestCase("Brushed Knit Scarf", "brushed-knit-scarf")]
    [TestCase("4 Pair Pack Cotton Rich Invisible Trainer Liner Socks", "4-pair-pack-cotton-rich-invisible-trainer-lin")]
    [TestCase("Sparkling Water Summer Fruits 1 Litre", "sparkling-water-summer-fruits-1-litre")]
    [TestCase("Fox And Ivy Egyptian Cotton Bath Towel Navy", "fox-and-ivy-egyptian-cotton-bath-towel-navy")]
    [TestCase("TRESemmé Hairspray Extra Hold 250ml", "tresemme-hairspray-extra-hold-250ml")]
    [TestCase("Francis Brennan the Collection Stripe Bone China Creamer", "francis-brennan-the-collection-stripe-bone-ch")]
    [TestCase("Summer Infant Grow with Me White Single Bed Rail", "summer-infant-grow-with-me-white-single-bed-r")]
    [TestCase("2 Pack Disney Frozen™ 2 Pyjama Set (2-10 Years)", "2-pack-disney-frozen-2-pyjama-set-2-10-years")]
    [TestCase("Heinz Vegetable Pot Soup 355G", "heinz-vegetable-pot-soup-355g")]
    [TestCase("Philips Hue White Ambience Runner 2 Spot w/ Dimmer - Black.", "philips-hue-white-ambience-runner-2-spot-w-di")]
    [TestCase("Nikon Z50 Mirrorless Camera with 16-50mm Lens", "nikon-z50-mirrorless-camera-with-16-50mm-lens")]
    [TestCase("YellowBelly Citra Pale Ale (440 Millilitre)", "yellowbelly-citra-pale-ale-440-millilitre")]
    [TestCase("Asparagus And Tender Stem Broccoli 190G", "asparagus-and-tender-stem-broccoli-190g")]
    [TestCase("Tommee Tippee Closer To Nature 6-18Mth Anytime Pink Soother X2", "tommee-tippee-closer-to-nature-6-18mth-anytim")]
    [TestCase("Warmers Houndstooth Hot Water Bottle", "warmers-houndstooth-hot-water-bottle")]
    [TestCase("Zip Firelighters (60 Piece)", "zip-firelighters-60-piece")]
    [TestCase("Wagtastic Toy Bobble Ball Dog Toy", "wagtastic-toy-bobble-ball-dog-toy")]
    [TestCase("USN Hyberbolic Mass Chocolate - 6kg", "usn-hyberbolic-mass-chocolate-6kg")]
    [TestCase("Wilsons Country Baby New Potatoes (750 Grams)", "wilsons-country-baby-new-potatoes-750-grams")]
    [TestCase("St Ives Invigorating Apricot Face Scrub 150ml", "st-ives-invigorating-apricot-face-scrub-150ml")]
    [TestCase("Schwarzkopf LIVE Color XXL HD 89 Bitter Sweet Chocolate Permanent Brown Hair Dye", "schwarzkopf-live-color-xxl-hd-89-bitter-sweet")]
    [TestCase("Melissa & doug Wooden Table & 2 Chairs", "melissa-doug-wooden-table-2-chairs")]
    [TestCase("Home Paolo 2 & 3 Seater Manual Recliner Sofas - Grey", "home-paolo-2-3-seater-manual-recliner-sofas")]
    [TestCase("Avent Milk Powder Dispenser", "avent-milk-powder-dispenser")]
    [TestCase("Always Soft & Fit Normal Plus (14 Piece)", "always-soft-fit-normal-plus-14-piece")]
    [TestCase("Reversible Red Brushed Check Double", "reversible-red-brushed-check-double")]
    [TestCase("Home 3ft Pre-Lit Iridescent Christmas Tree - White", "home-3ft-pre-lit-iridescent-christmas-tree")]
    [TestCase("Boys Sporty Hi-Tops", "boys-sporty-hi-tops")]
    [TestCase("Glenisk Organic Yogurt Natural With Milk (500 Grams)", "glenisk-organic-yogurt-natural-with-milk-500")]
    [TestCase("Joanne Hynes Grey Cashmere Blend Tiger Lady Hat", "joanne-hynes-grey-cashmere-blend-tiger-lady-h")]
    public void When_GenerateSlug_Ok(string name, string expected) => Assert.That(name.GenerateSlug(), Is.EqualTo(expected));
}