using CDPAutomation.Enums.FindElement;

namespace CDPAutomation.Extensions
{
    internal static class DelayExtensions
    {
        internal static int GetDelay(this KeyboardAction action)
        {
            return action switch
            {
                KeyboardAction.Fast => 10,
                KeyboardAction.Slow => 100,
                KeyboardAction.RandomDelay => new Random().Next(50, 200),
                _ => 50,
            };
        }
    }
}
