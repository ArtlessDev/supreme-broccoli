//Code for QuestBoard
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using SupremeBroccoli.Components.Controls;
using System.Linq;
namespace SupremeBroccoli.Screens;
partial class QuestBoard : global::Gum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("QuestBoard") ?? throw new System.InvalidOperationException("Could not find an element named QuestBoard - did you forget to load a Gum project?");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new QuestBoard(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(QuestBoard)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("QuestBoard", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime NineSliceInstance { get; protected set; }
    public TextRuntime BoardTitle { get; protected set; }
    public StackPanel StackPanel { get; protected set; }

    public QuestBoard(InteractiveGue visual) : base(visual)
    {
        InitializeInstances();
        CustomInitialize();
    }
    public QuestBoard() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    public virtual void InitializeInstances()
    {
        NineSliceInstance = new global::MonoGameGum.GueDeriving.NineSliceRuntime();
        NineSliceInstance.ElementSave = ObjectFinder.Self.GetStandardElement("NineSlice");
        if (NineSliceInstance.ElementSave != null) NineSliceInstance.AddStatesAndCategoriesRecursivelyToGue(NineSliceInstance.ElementSave);
        if (NineSliceInstance.ElementSave != null) NineSliceInstance.SetInitialState();
        NineSliceInstance.Name = "NineSliceInstance";
        BoardTitle = new global::MonoGameGum.GueDeriving.TextRuntime();
        BoardTitle.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (BoardTitle.ElementSave != null) BoardTitle.AddStatesAndCategoriesRecursivelyToGue(BoardTitle.ElementSave);
        if (BoardTitle.ElementSave != null) BoardTitle.SetInitialState();
        BoardTitle.Name = "BoardTitle";
        StackPanel = new SupremeBroccoli.Components.Controls.StackPanel();
        StackPanel.Name = "StackPanel";
        base.RefreshInternalVisualReferences();
    }
    public virtual void AssignParents()
    {
        this.AddChild(NineSliceInstance);
        this.AddChild(BoardTitle);
        this.AddChild(StackPanel);
    }
    public void ApplyDefaultVariables()
    {
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.NineSliceInstance.Height = 90f;
        this.NineSliceInstance.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.NineSliceInstance.IsTilingMiddleSections = true;
        this.NineSliceInstance.SourceFileName = @"KenneyAssets\panel_brown.png";
        this.NineSliceInstance.Width = 90f;
        this.NineSliceInstance.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.NineSliceInstance.X = 5f;
        this.NineSliceInstance.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.NineSliceInstance.Y = 5f;
        this.NineSliceInstance.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.BoardTitle.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.BoardTitle.FontSize = 36;
        ((TextRuntime)this.BoardTitle).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.BoardTitle.Text = @"Quest Board";
        this.BoardTitle.Width = 244f;
        this.BoardTitle.X = 10f;
        this.BoardTitle.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.BoardTitle.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.BoardTitle.Y = 10f;
        this.BoardTitle.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.StackPanel.Visual.Height = 70f;
        this.StackPanel.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.StackPanel.Visual.Width = 80f;
        this.StackPanel.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.StackPanel.Visual.X = 10f;
        this.StackPanel.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.StackPanel.Visual.Y = 20f;
        this.StackPanel.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

    }
    partial void CustomInitialize();
}
