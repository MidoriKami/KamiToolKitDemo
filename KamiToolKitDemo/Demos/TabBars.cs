using System.Numerics;
using FFXIVClientStructs.FFXIV.Component.GUI;
using KamiToolKit.Nodes;

namespace KamiToolKitDemo.Demos;

public static class TabBars {
	public static void TabBarDemo(TreeListCategoryNode tabBarCategory) {
		tabBarCategory.AddHeader("TabBar");
		TabBar(tabBarCategory);
	}

	private static void TabBar(TreeListCategoryNode tabBarCategory) {
		var container = new VerticalListNode {
			Size = new Vector2(tabBarCategory.Width - 8.0f, 0.0f),
			Position = new Vector2(4.0f, 0.0f),
			FitContents = true,
		};

		var tabContainer = new ResNode {
			Size = new Vector2(tabBarCategory.Width, 28.0f), 
		};

		var textNode1 = new TextNode {
			TextFlags = TextFlags.AutoAdjustNodeSize, 
			String = "First Tab Element", 
		};
		
		var textNode2 = new TextNode {
			TextFlags = TextFlags.AutoAdjustNodeSize, 
			String = "Second Tab Element", 
			IsVisible = false, 
		};
		
		var textNode3 = new TextNode {
			TextFlags = TextFlags.AutoAdjustNodeSize,
			String = "Third Tab Element", 
			IsVisible = false, 
		};
		
		// The tab bar itself is a very simple node, it only provides the ability to .AddTab with a String
		// and to set up onclick events when a tab is clicked
		var tabBar = new TabBarNode {
			Size = new Vector2(tabBarCategory.Width - 8.0f, 24.0f), 
		};

		tabBar.AddTab("First", () => {
			textNode1.IsVisible = true;
			textNode2.IsVisible = false;
			textNode3.IsVisible = false;
		});
		
		tabBar.AddTab("Second", () => {
			textNode1.IsVisible = false;
			textNode2.IsVisible = true;
			textNode3.IsVisible = false;
		});
		
		tabBar.AddTab("Third", () => {
			textNode1.IsVisible = false;
			textNode2.IsVisible = false;
			textNode3.IsVisible = true;
		});
		
		textNode1.AttachNode(tabContainer);
		textNode2.AttachNode(tabContainer);
		textNode3.AttachNode(tabContainer);
		
		container.AddNode(tabBar);
		container.AddNode(tabContainer);
		tabBarCategory.AddNode(container);
	}
}