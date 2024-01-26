namespace CramerClassification;

public record BinaryDecisionTreeNode(
    Func<Chemical, bool> Predicate,
    DecisionTreeNode TrueEdge,
    DecisionTreeNode FalseEdge) : DecisionTreeNode
{
    public override ToxicityRisk Evaluate(Chemical chemical) => 
        Predicate(chemical) ? TrueEdge.Evaluate(chemical) : FalseEdge.Evaluate(chemical);
}