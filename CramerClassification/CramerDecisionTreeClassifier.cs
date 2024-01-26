namespace CramerClassification;

public class CramerDecisionTreeClassifier
{
    private static readonly DecisionTreeNode RootDecisionTreeNode = new AnyRedCircle();
    
    public ToxicityRisk Classify(Chemical chemical) => RootDecisionTreeNode.Evaluate(chemical);

    private record AnyRedCircle() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAnyRedCircles(),
            HighRisk, 
            new AllBlueCircles());

    private record AllBlueCircles() :
        BinaryDecisionTreeNode(
            chemical => chemical.IsAllBlueCircles(),
            new SmallestCircleRadiusAtLeast50(),
            new AnyRectangles());

    private record SmallestCircleRadiusAtLeast50() :
        BinaryDecisionTreeNode(
            chemical => chemical.SmallestCircleRadius() > 50,
            new SmallestCircleRadiusAtLeast100(),
            HighRisk);

    private record SmallestCircleRadiusAtLeast100() :
        BinaryDecisionTreeNode(
            chemical => chemical.SmallestCircleRadius() > 100,
            LowRisk, 
            MediumRisk);

    private record AnyRectangles() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAny<Rect>(),
            new RectanglesAndAnyText(),
            new NoRectanglesAndAnyText());

    private record NoRectanglesAndAnyText() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAny<Text>(),
            new AllTextContainsLhasa(),
            MediumRisk);

    private record AllTextContainsLhasa() :
        BinaryDecisionTreeNode(
            chemical => chemical.All<Text>(t => t.Value.Contains("lhasa")),
            LowRisk, 
            HighRisk);

    private record RectanglesAndAnyText() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAny<Text>(),
            new AllRectanglesAreGreen(),
            MoreThanOneElement.Instance);

    private record AllRectanglesAreGreen() :
        BinaryDecisionTreeNode(
            chemical => chemical.All<Rect>(r => r.Colour == Colour.Green),
            LowRisk,
            MoreThanOneElement.Instance);

    private record MoreThanOneElement() :
        BinaryDecisionTreeNode(
            chemical => chemical.Groups.Length > 1,
            new AnyLines(),
            LowRisk)
    {
        public static readonly MoreThanOneElement Instance = new();
    }

    private record AnyLines() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAny<Line>(),
            new AllLinesHaveLengthLongerThan100(),
            new AnyEllipses());

    private record AllLinesHaveLengthLongerThan100() :
        BinaryDecisionTreeNode(
            chemical => chemical.All<Line>(x => x.Length > 100),
            MediumRisk,
            HighRisk);

    private record AnyEllipses() :
        BinaryDecisionTreeNode(
            chemical => chemical.HasAny<Ellipse>(),
            new AnyEllipseHasHeight50OrMore(),
            new AnyElementWithOpacityLessThanOne());

    private record AnyEllipseHasHeight50OrMore() :
        BinaryDecisionTreeNode(
            chemical => chemical.Any<Ellipse>(e => e.Height >= 50),
            HighRisk,
            new AnyRectangleHasArea300OrMore());

    private record AnyRectangleHasArea300OrMore() :
        BinaryDecisionTreeNode(
            chemical => chemical.Any<Rect>(r => r.Area >= 300),
            LowRisk,
            new MoreThan5Elements());

    private record MoreThan5Elements() :
        BinaryDecisionTreeNode(
            chemical => chemical.Groups.Length > 5,
            MediumRisk,
            HighRisk);

    private record AnyElementWithOpacityLessThanOne() :
        BinaryDecisionTreeNode(
            chemical => chemical.Groups.Any(x => x.Opacity < 1),
            new LowOpacityAndMoreThan5Elements(),
            HighRisk);

    private record LowOpacityAndMoreThan5Elements() :
        BinaryDecisionTreeNode(
            chemical => chemical.Groups.Length > 5,
            LowRisk,
            MediumRisk);
}