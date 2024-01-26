namespace CramerClassification;

public record Circle(Colour Colour, int Radius, double Opacity = 1) : FunctionalGroup(Colour, Opacity);