using System.Numerics;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using KamiToolKit;
using KamiToolKitDemo.Demos;

namespace KamiToolKitDemo;

public sealed class KamiToolKitDemo : IDalamudPlugin {
    public KamiToolKitDemo(IDalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        // Native Controller is required for injecting KamiToolKit elements into the native UI
        // It provides tracking and safety features that ensure the stability of the game when manipulating native elements
        KamiToolKitLibrary.Initialize(pluginInterface, "KamiToolKit Demo");

        System.AddonWidgetDemo = new AddonWidgetDemo {
            InternalName = "AddonWidgetDemo", 
            Title = "Widget Demo", 
            Size = new Vector2(750.0f, 750.0f), 
        };
        
        // For this demo, we will open the widget window as soon as the plugin loads
        OpenWidgetDemo();

        // We will also add a chat command to open this window
        Services.CommandManager.AddHandler("/widget", new CommandInfo(OnCommand) {
            HelpMessage = "Open Widget Window",
        });

        Services.PluginInterface.UiBuilder.OpenConfigUi += System.AddonWidgetDemo.Toggle;
        Services.PluginInterface.UiBuilder.OpenMainUi += System.AddonWidgetDemo.Toggle;
    }

    private void OnCommand(string command, string arguments) {
        switch (command) {
            case "/widget":
                OpenWidgetDemo();
                break;
        }
    }

    public void Dispose() {
        Services.PluginInterface.UiBuilder.OpenConfigUi -= System.AddonWidgetDemo.Toggle;
        Services.PluginInterface.UiBuilder.OpenMainUi -= System.AddonWidgetDemo.Toggle;
        
        // Disposing our Addon will close it and remove it from the game
        System.AddonWidgetDemo.Dispose();
        
        // Disposing KamiToolKitLibrary will attempt to clean up any nodes or addons that we might have missed
        KamiToolKitLibrary.Dispose();
    }
    
    private void OpenWidgetDemo()
        => System.AddonWidgetDemo.Open();
}

// A simple static class for storing any dalamud services that we might want, and for storing any objects that we'll need
public class Services {
    [PluginService] public static ICommandManager CommandManager { get; set; } = null!;
	[PluginService] public static IDalamudPluginInterface PluginInterface { get; set; } = null!;
	[PluginService] public static IDataManager DataManager { get; set; } = null!;
}

public static class System {
    public static AddonWidgetDemo AddonWidgetDemo { get; set; } = null!;
}
