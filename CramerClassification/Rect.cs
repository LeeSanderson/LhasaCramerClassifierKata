namespace CramerClassification;

public record Rect(Colour Colour, int Height, int Width, double Opacity = 1): FunctionalGroup(Colour, Opacity)
{
    public int Area => Height * Width;
}