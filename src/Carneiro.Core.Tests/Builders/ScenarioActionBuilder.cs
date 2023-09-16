namespace Carneiro.Core.Tests.Builders;

/// <summary>
/// The scenario action builder for multiple entities.
/// </summary>
public static class ScenarioActionBuilder
{
    /// <summary>
    /// Builds the actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="numberOfItems">The number of items.</param>
    /// <param name="defaultAction">The default action.</param>
    /// <param name="actions">The actions.</param>
    /// <returns></returns>
    public static ICollection<Action<T>> BuildActions<T>(int numberOfItems, Action<T> defaultAction, ICollection<Action<T>> actions)
    {
        if (defaultAction is null)
            return actions;

        ICollection<Action<T>> builtActions = new List<Action<T>>(numberOfItems);

        for (int i = 0; i < numberOfItems; i++)
        {
            builtActions.Add(defaultAction);
        }

        return builtActions;
    }

    /// <summary>
    /// Builds the actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="numberOfItems">The number of items.</param>
    /// <param name="defaultAction">The default action.</param>
    public static ICollection<Action<T>> BuildActions<T>(int numberOfItems, Action<T> defaultAction)
    {
        ICollection<Action<T>> builtActions = new List<Action<T>>(numberOfItems);

        for (int i = 0; i < numberOfItems; i++)
        {
            builtActions.Add(defaultAction);
        }

        return builtActions;
    }
}