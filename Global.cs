global using static DarksTowers.Global;
using BTD_Mod_Helper;

namespace DarksTowers;

internal static class Global
{
    public static void Log(object message)
    {
        ModHelper.Log<Main>(message);
    }
}