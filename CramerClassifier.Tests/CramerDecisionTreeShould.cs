using CramerClassification;

namespace CramerClassifier.Tests;

public class CramerDecisionTreeShould
{
    private readonly CramerDecisionTreeClassifier _classifier = new();

    [Fact]
    public void ReturnHighRiskIfAnyRedCircles()
    {
        var chemical = new Chemical(new Circle(Colour.Red, 100));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnLowRiskIfJustBlueCirclesWithRadiusLargerThan100()
    {
        var chemical = new Chemical(new Circle(Colour.Blue, 101));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }

    [Fact]
    public void ReturnMediumRiskIfJustBlueCirclesWithRadiusBetween50And100()
    {
        var chemical = new Chemical(new Circle(Colour.Blue, 100));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Medium);
    }

    [Fact]
    public void ReturnHighRiskIfJustBlueCirclesWithRadiusLessThan50()
    {
        var chemical = new Chemical(new Circle(Colour.Blue, 49));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnLowRiskIfGreenRectanglesAndText()
    {
        var chemical = new Chemical(
            new Rect(Colour.Green, 10, 10), 
            new Text(Colour.Black, "Hello"));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }

    [Fact]
    public void ReturnHighRiskIfNonGreenRectangleAndText()
    {
        var chemical = new Chemical(
            new Rect(Colour.Black, 10, 10),
            new Text(Colour.Black, "Hello"));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnLowRiskIfRectangles()
    {
        var chemical = new Chemical(new Rect(Colour.Black, 10, 10));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }

    [Fact]
    public void ReturnMediumRiskIfNoRectanglesOrText()
    {
        var chemical = new Chemical(new Circle(Colour.Green, 10));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Medium);
    }

    [Fact]
    public void ReturnLowRiskIfNoRectanglesAndTextContainingLhasa()
    {
        var chemical = 
            new Chemical(
                new Circle(Colour.Green, 10), 
                new Text(Colour.Black, "Text with lhasa in the middle"));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }

    [Fact]
    public void ReturnHighRiskIfNoRectanglesAndTextDoesNotContainLhasa()
    {
        var chemical =
            new Chemical(
                new Circle(Colour.Green, 10),
                new Text(Colour.Black, "Text without the magic word"));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnHighRiskIfRectanglesAndLinesShorterThan100()
    {
        var chemical = 
            new Chemical(
                new Rect(Colour.Black, 10, 10),
                new Line(Colour.Black, 0, 0, 50, 0));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnMediumRiskIfRectanglesAndLinesLongerThan100()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10),
                new Line(Colour.Black, 0, 0, 101, 0));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Medium);
    }

    [Fact]
    public void ReturnHighRiskIfRectanglesAndEllipseWithHeight50OrMore()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10),
                new Ellipse(Colour.Black, 50));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnLowRiskIfEllipseAndRectangleWithArea300OrMore()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 30, 10),
                new Ellipse(Colour.Black, 30));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }

    [Fact]
    public void ReturnMediumRiskIfMoreThan5EllipsesAndRectangles()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10),
                new Rect(Colour.Black, 10, 10),
                new Rect(Colour.Black, 10, 10),
                new Ellipse(Colour.Black, 30),
                new Ellipse(Colour.Black, 30),
                new Ellipse(Colour.Black, 30));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Medium);
    }

    [Fact]
    public void ReturnHighRiskIf5OrFewerEllipsesAndRectangles()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10),
                new Rect(Colour.Black, 10, 10),
                new Rect(Colour.Black, 10, 10),
                new Ellipse(Colour.Black, 30),
                new Ellipse(Colour.Black, 30));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnHighRiskIfNoElementsWithOpacityLessThanOne()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10, 1.1),
                new Circle(Colour.Black, 10));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.High);
    }

    [Fact]
    public void ReturnMediumRiskIfSomeElementsHaveOpacityLessThanOneAndLessThan5Items()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10, 0.5),
                new Circle(Colour.Black, 10));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Medium);
    }

    [Fact]
    public void ReturnLowRiskIfSomeElementsHaveOpacityLessThanOneAndMoreThan5Items()
    {
        var chemical =
            new Chemical(
                new Rect(Colour.Black, 10, 10, 0.5),
                new Rect(Colour.Black, 10, 10, 0.5),
                new Rect(Colour.Black, 10, 10, 0.5),
                new Rect(Colour.Black, 10, 10, 0.5),
                new Rect(Colour.Black, 10, 10, 0.5),
                new Circle(Colour.Black, 10));
        var risk = _classifier.Classify(chemical);
        risk.Should().Be(ToxicityRisk.Low);
    }
}