//Code for QuestBoard
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using SupremeBroccoli.Components;
using SupremeBroccoli.Components.Controls;
using System.Linq;
namespace SupremeBroccoli.Screens;
[global::Gum.DataTypes.GumElement("QuestBoard")]
partial class QuestBoard : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("QuestBoard");
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
    public QuestOnBoard QuestOnBoardInstance3 { get; protected set; }
    public TextRuntime BoardTitle { get; protected set; }
    public StackPanel StackPanelInstance { get; protected set; }

    public QuestBoard(InteractiveGue visual) : base(visual)
    {
        InitializeInstancesFromVisual();
        CustomInitialize();
    }
    public QuestBoard() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        QuestOnBoardInstance3 = new SupremeBroccoli.Components.QuestOnBoard();
        QuestOnBoardInstance3.Name = "QuestOnBoardInstance3";
        BoardTitle = new global::Gum.GueDeriving.TextRuntime();
        BoardTitle.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (BoardTitle.ElementSave != null) BoardTitle.AddStatesAndCategoriesRecursivelyToGue(BoardTitle.ElementSave);
        if (BoardTitle.ElementSave != null) BoardTitle.SetInitialState();
        BoardTitle.Name = "BoardTitle";
        StackPanelInstance = new SupremeBroccoli.Components.Controls.StackPanel();
        StackPanelInstance.Name = "StackPanelInstance";
    }
    protected virtual void InitializeInstancesFromVisual()
    {
        QuestOnBoardInstance3 = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<QuestOnBoard>(this.Visual,"QuestOnBoardInstance3");
        BoardTitle = this.Visual?.GetGraphicalUiElementByName("BoardTitle") as global::Gum.GueDeriving.TextRuntime;
        StackPanelInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<StackPanel>(this.Visual,"StackPanelInstance");
    }
    protected virtual void AssignParents()
    {
        this.AddChild(QuestOnBoardInstance3);
        this.AddChild(BoardTitle);
        this.AddChild(StackPanelInstance);
    }
    protected virtual void ApplyDefaultVariables()
    {
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.QuestOnBoardInstance3.Visual.X = 55f;
        this.QuestOnBoardInstance3.Visual.Y = 484f;

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

        this.StackPanelInstance.Visual.Height = 70f;
        this.StackPanelInstance.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.StackPanelInstance.Visual.Width = 80f;
        this.StackPanelInstance.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.StackPanelInstance.Visual.X = 10f;
        this.StackPanelInstance.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.StackPanelInstance.Visual.Y = 20f;
        this.StackPanelInstance.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

    }
    partial void CustomInitialize();
}
