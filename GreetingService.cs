namespace Demo.Components;

public static class GreetingService
{
    public static string GetMessage(string name)
    {
        return $"Hello {name} from Demo.Components development build";
    }
}
