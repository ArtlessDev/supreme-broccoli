//Code for CombatScreen
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using SupremeBroccoli.Components.Controls;
using System.Linq;
namespace SupremeBroccoli.Screens;
[global::Gum.DataTypes.GumElement("CombatScreen")]
partial class CombatScreen : global::Gum.Forms.Controls.FrameworkElement
{
    #if UNITY_5_3_OR_NEWER && !GUM_NO_UNITY_ENGINE
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
    #else
    [System.Runtime.CompilerServices.ModuleInitializer]
    #endif
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::Gum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetRequiredElementSave("CombatScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new CombatScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(CombatScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("CombatScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public SpriteRuntime SpriteInstance2 { get; protected set; }
    public SpriteRuntime SpriteInstance1 { get; protected set; }
    public DialogBox BottomMiddle_DBox { get; protected set; }
    public ContainerRuntime PlayerDetails { get; protected set; }
    public TextRuntime PlayerName { get; protected set; }
    public TextRuntime PlayerHealth { get; protected set; }
    public TextRuntime PlayerStatus { get; protected set; }
    public TextRuntime PlayerName1 { get; protected set; }
    public TextRuntime PlayerHealth1 { get; protected set; }
    public TextRuntime PlayerStatus1 { get; protected set; }
    public ContainerRuntime EnemyContainer { get; protected set; }
    public ContainerRuntime EnemyDetails { get; protected set; }
    public SpriteRuntime EnemySprite { get; protected set; }

    public CombatScreen(InteractiveGue visual) : base(visual)
    {
        InitializeInstancesFromVisual();
        CustomInitialize();
    }
    public CombatScreen() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        SpriteInstance2 = new global::Gum.GueDeriving.SpriteRuntime();
        SpriteInstance2.ElementSave = ObjectFinder.Self.GetStandardElement("Sprite");
        if (SpriteInstance2.ElementSave != null) SpriteInstance2.AddStatesAndCategoriesRecursivelyToGue(SpriteInstance2.ElementSave);
        if (SpriteInstance2.ElementSave != null) SpriteInstance2.SetInitialState();
        SpriteInstance2.Name = "SpriteInstance2";
        SpriteInstance1 = new global::Gum.GueDeriving.SpriteRuntime();
        SpriteInstance1.ElementSave = ObjectFinder.Self.GetStandardElement("Sprite");
        if (SpriteInstance1.ElementSave != null) SpriteInstance1.AddStatesAndCategoriesRecursivelyToGue(SpriteInstance1.ElementSave);
        if (SpriteInstance1.ElementSave != null) SpriteInstance1.SetInitialState();
        SpriteInstance1.Name = "SpriteInstance1";
        BottomMiddle_DBox = new SupremeBroccoli.Components.Controls.DialogBox();
        BottomMiddle_DBox.Name = "BottomMiddle_DBox";
        PlayerDetails = new global::Gum.GueDeriving.ContainerRuntime();
        PlayerDetails.ElementSave = ObjectFinder.Self.GetStandardElement("Container");
        if (PlayerDetails.ElementSave != null) PlayerDetails.AddStatesAndCategoriesRecursivelyToGue(PlayerDetails.ElementSave);
        if (PlayerDetails.ElementSave != null) PlayerDetails.SetInitialState();
        PlayerDetails.Name = "PlayerDetails";
        PlayerName = new global::Gum.GueDeriving.TextRuntime();
        PlayerName.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerName.ElementSave != null) PlayerName.AddStatesAndCategoriesRecursivelyToGue(PlayerName.ElementSave);
        if (PlayerName.ElementSave != null) PlayerName.SetInitialState();
        PlayerName.Name = "PlayerName";
        PlayerHealth = new global::Gum.GueDeriving.TextRuntime();
        PlayerHealth.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerHealth.ElementSave != null) PlayerHealth.AddStatesAndCategoriesRecursivelyToGue(PlayerHealth.ElementSave);
        if (PlayerHealth.ElementSave != null) PlayerHealth.SetInitialState();
        PlayerHealth.Name = "PlayerHealth";
        PlayerStatus = new global::Gum.GueDeriving.TextRuntime();
        PlayerStatus.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerStatus.ElementSave != null) PlayerStatus.AddStatesAndCategoriesRecursivelyToGue(PlayerStatus.ElementSave);
        if (PlayerStatus.ElementSave != null) PlayerStatus.SetInitialState();
        PlayerStatus.Name = "PlayerStatus";
        PlayerName1 = new global::Gum.GueDeriving.TextRuntime();
        PlayerName1.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerName1.ElementSave != null) PlayerName1.AddStatesAndCategoriesRecursivelyToGue(PlayerName1.ElementSave);
        if (PlayerName1.ElementSave != null) PlayerName1.SetInitialState();
        PlayerName1.Name = "PlayerName1";
        PlayerHealth1 = new global::Gum.GueDeriving.TextRuntime();
        PlayerHealth1.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerHealth1.ElementSave != null) PlayerHealth1.AddStatesAndCategoriesRecursivelyToGue(PlayerHealth1.ElementSave);
        if (PlayerHealth1.ElementSave != null) PlayerHealth1.SetInitialState();
        PlayerHealth1.Name = "PlayerHealth1";
        PlayerStatus1 = new global::Gum.GueDeriving.TextRuntime();
        PlayerStatus1.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (PlayerStatus1.ElementSave != null) PlayerStatus1.AddStatesAndCategoriesRecursivelyToGue(PlayerStatus1.ElementSave);
        if (PlayerStatus1.ElementSave != null) PlayerStatus1.SetInitialState();
        PlayerStatus1.Name = "PlayerStatus1";
        EnemyContainer = new global::Gum.GueDeriving.ContainerRuntime();
        EnemyContainer.ElementSave = ObjectFinder.Self.GetStandardElement("Container");
        if (EnemyContainer.ElementSave != null) EnemyContainer.AddStatesAndCategoriesRecursivelyToGue(EnemyContainer.ElementSave);
        if (EnemyContainer.ElementSave != null) EnemyContainer.SetInitialState();
        EnemyContainer.Name = "EnemyContainer";
        EnemyDetails = new global::Gum.GueDeriving.ContainerRuntime();
        EnemyDetails.ElementSave = ObjectFinder.Self.GetStandardElement("Container");
        if (EnemyDetails.ElementSave != null) EnemyDetails.AddStatesAndCategoriesRecursivelyToGue(EnemyDetails.ElementSave);
        if (EnemyDetails.ElementSave != null) EnemyDetails.SetInitialState();
        EnemyDetails.Name = "EnemyDetails";
        EnemySprite = new global::Gum.GueDeriving.SpriteRuntime();
        EnemySprite.ElementSave = ObjectFinder.Self.GetStandardElement("Sprite");
        if (EnemySprite.ElementSave != null) EnemySprite.AddStatesAndCategoriesRecursivelyToGue(EnemySprite.ElementSave);
        if (EnemySprite.ElementSave != null) EnemySprite.SetInitialState();
        EnemySprite.Name = "EnemySprite";
    }
    protected virtual void InitializeInstancesFromVisual()
    {
        SpriteInstance2 = this.Visual?.GetGraphicalUiElementByName("SpriteInstance2") as global::Gum.GueDeriving.SpriteRuntime;
        SpriteInstance1 = this.Visual?.GetGraphicalUiElementByName("SpriteInstance1") as global::Gum.GueDeriving.SpriteRuntime;
        BottomMiddle_DBox = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<DialogBox>(this.Visual,"BottomMiddle_DBox");
        PlayerDetails = this.Visual?.GetGraphicalUiElementByName("PlayerDetails") as global::Gum.GueDeriving.ContainerRuntime;
        PlayerName = this.Visual?.GetGraphicalUiElementByName("PlayerName") as global::Gum.GueDeriving.TextRuntime;
        PlayerHealth = this.Visual?.GetGraphicalUiElementByName("PlayerHealth") as global::Gum.GueDeriving.TextRuntime;
        PlayerStatus = this.Visual?.GetGraphicalUiElementByName("PlayerStatus") as global::Gum.GueDeriving.TextRuntime;
        PlayerName1 = this.Visual?.GetGraphicalUiElementByName("PlayerName1") as global::Gum.GueDeriving.TextRuntime;
        PlayerHealth1 = this.Visual?.GetGraphicalUiElementByName("PlayerHealth1") as global::Gum.GueDeriving.TextRuntime;
        PlayerStatus1 = this.Visual?.GetGraphicalUiElementByName("PlayerStatus1") as global::Gum.GueDeriving.TextRuntime;
        EnemyContainer = this.Visual?.GetGraphicalUiElementByName("EnemyContainer") as global::Gum.GueDeriving.ContainerRuntime;
        EnemyDetails = this.Visual?.GetGraphicalUiElementByName("EnemyDetails") as global::Gum.GueDeriving.ContainerRuntime;
        EnemySprite = this.Visual?.GetGraphicalUiElementByName("EnemySprite") as global::Gum.GueDeriving.SpriteRuntime;
    }
    protected virtual void AssignParents()
    {
        EnemyDetails.AddChild(SpriteInstance2);
        PlayerDetails.AddChild(SpriteInstance1);
        this.AddChild(BottomMiddle_DBox);
        this.AddChild(PlayerDetails);
        PlayerDetails.AddChild(PlayerName);
        PlayerDetails.AddChild(PlayerHealth);
        PlayerDetails.AddChild(PlayerStatus);
        EnemyDetails.AddChild(PlayerName1);
        EnemyDetails.AddChild(PlayerHealth1);
        EnemyDetails.AddChild(PlayerStatus1);
        this.AddChild(EnemyContainer);
        EnemyContainer.AddChild(EnemyDetails);
        EnemyContainer.AddChild(EnemySprite);
    }
    protected virtual void ApplyDefaultVariables()
    {
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.SpriteInstance2.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.SpriteInstance2.SourceFileName = @"KenneyAssets\panel_blue.png";
        this.SpriteInstance2.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.SpriteInstance2.X = 0f;
        this.SpriteInstance2.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.SpriteInstance2.Y = 0f;
        this.SpriteInstance2.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.SpriteInstance1.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.SpriteInstance1.SourceFileName = @"KenneyAssets\panel_blue.png";
        this.SpriteInstance1.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.SpriteInstance1.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.SpriteInstance1.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.BottomMiddle_DBox.Visual.Height = 25f;
        this.BottomMiddle_DBox.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.BottomMiddle_DBox.Visual.Width = 80f;
        this.BottomMiddle_DBox.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.BottomMiddle_DBox.Visual.X = 0f;
        this.BottomMiddle_DBox.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.BottomMiddle_DBox.Visual.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromMiddle;
        this.BottomMiddle_DBox.Visual.Y = 95f;
        this.BottomMiddle_DBox.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Bottom;
        this.BottomMiddle_DBox.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.PlayerDetails.Height = 20f;
        this.PlayerDetails.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerDetails.Width = 20f;
        this.PlayerDetails.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerDetails.X = 5f;
        this.PlayerDetails.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.PlayerDetails.Y = 0f;
        this.PlayerDetails.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Bottom;
        this.PlayerDetails.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromBaseline;

        this.PlayerName.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerName.FontSize = 36;
        this.PlayerName.Height = 25f;
        this.PlayerName.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerName).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerName.Text = @"player name";
        this.PlayerName.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;

        this.PlayerHealth.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerHealth.FontSize = 36;
        this.PlayerHealth.Height = 50f;
        this.PlayerHealth.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerHealth).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerHealth.Text = @"player health";
        this.PlayerHealth.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerHealth.X = 0f;
        this.PlayerHealth.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.PlayerHealth.Y = 30f;
        this.PlayerHealth.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.PlayerStatus.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerStatus.FontSize = 36;
        this.PlayerStatus.Height = 50f;
        this.PlayerStatus.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerStatus).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerStatus.Text = @"player status";
        this.PlayerStatus.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerStatus.X = 0f;
        this.PlayerStatus.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.PlayerStatus.Y = 60f;
        this.PlayerStatus.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.PlayerName1.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerName1.FontSize = 36;
        this.PlayerName1.Height = 25f;
        this.PlayerName1.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerName1).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerName1.Text = @"player name";
        this.PlayerName1.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;

        this.PlayerHealth1.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerHealth1.FontSize = 36;
        this.PlayerHealth1.Height = 50f;
        this.PlayerHealth1.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerHealth1).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerHealth1.Text = @"player health";
        this.PlayerHealth1.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerHealth1.X = 0f;
        this.PlayerHealth1.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.PlayerHealth1.Y = 30f;
        this.PlayerHealth1.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.PlayerStatus1.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.PlayerStatus1.FontSize = 36;
        this.PlayerStatus1.Height = 50f;
        this.PlayerStatus1.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.PlayerStatus1).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.PlayerStatus1.Text = @"player status";
        this.PlayerStatus1.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.PlayerStatus1.X = 0f;
        this.PlayerStatus1.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.PlayerStatus1.Y = 60f;
        this.PlayerStatus1.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.EnemyContainer.Height = 512f;
        this.EnemyContainer.Width = 512f;
        this.EnemyContainer.WidthUnits = global::Gum.DataTypes.DimensionUnitType.Absolute;
        this.EnemyContainer.X = 50f;
        this.EnemyContainer.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.EnemyContainer.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.EnemyContainer.Y = 5f;
        this.EnemyContainer.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.EnemyDetails.Height = 20f;
        this.EnemyDetails.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.EnemyDetails.Width = 100f;
        this.EnemyDetails.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.EnemyDetails.X = 0f;
        this.EnemyDetails.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.EnemyDetails.Y = 0f;
        this.EnemyDetails.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Bottom;
        this.EnemyDetails.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromBaseline;

        this.EnemySprite.Height = 100f;
        this.EnemySprite.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.EnemySprite.SourceFileName = @"..\CombatSprites\magicmouse.png";
        this.EnemySprite.Width = 100f;
        this.EnemySprite.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.EnemySprite.X = 0f;
        this.EnemySprite.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.EnemySprite.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.EnemySprite.Y = 0f;
        this.EnemySprite.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.EnemySprite.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

    }
    partial void CustomInitialize();
}
