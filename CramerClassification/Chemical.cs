namespace CramerClassification;

public record Chemical(params FunctionalGroup[] Groups)
{
    public int SmallestCircleRadius()
    {
        var circles = Groups.Cast<Circle>();
        return circles.Min(c => c.Radius);
    }

    public bool IsAllBlueCircles() => 
        Groups.All(fg => fg is Circle {Colour: Colour.Blue});

    public bool HasAnyRedCircles() =>
        Groups.Any(fg => fg is Circle {Colour: Colour.Red});

    public bool HasAny<T>() where T : FunctionalGroup => 
        Groups.Any(fg => fg is T);
    
    public bool All<T>(Func<T, bool> predicate) where T: FunctionalGroup =>
        Groups
            .Where(fg => fg is T)
            .Cast<T>()
            .All(predicate);

    public bool Any<T>(Func<T, bool> predicate) where T : FunctionalGroup =>
        Groups
            .Where(fg => fg is T)
            .Cast<T>()
            .Any(predicate);
}


// public static class ObjectExtensions
// {
//     public static BoolChain<T> Is<T>(this object value)
//     {
//         return value is T t ? new BoolChain<T>(t, _ => true) : BoolChain<T>.False;
//     }
// }
//
// public class BoolChain<T>(T value, Predicate<T> evaluator)
// {
//     public static readonly BoolChain<T> False = new(default!, _ => false);
//
//     public BoolChain<T> And(Predicate<T> chain)
//     {
//         return new BoolChain<T>(value, t => evaluator(t) && chain(t));
//     }
//
//     public bool IsTrue => evaluator(value);
//     
//     public static implicit operator bool(BoolChain<T> c) => c.IsTrue;
// }