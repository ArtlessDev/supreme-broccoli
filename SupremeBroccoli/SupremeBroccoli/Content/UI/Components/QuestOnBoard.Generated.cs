//Code for QuestOnBoard (Container)
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
namespace SupremeBroccoli.Components;
[global::Gum.DataTypes.GumElement("QuestOnBoard")]
partial class QuestOnBoard : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("QuestOnBoard");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new QuestOnBoard(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(QuestOnBoard)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("QuestOnBoard", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ButtonStandard RejectQuestButton { get; protected set; }
    public ButtonStandard AcceptQuestButton { get; protected set; }
    public TextRuntime QuestTitle { get; set; }
    public TextRuntime QuestDetails { get; set; }
    public StackPanel StackPanelInstance { get; protected set; }

    public QuestOnBoard(InteractiveGue visual) : base(visual)
    {
        InitializeInstancesFromVisual();
        ReactToGeneratedVisual();
        CustomInitialize();
    }
    public QuestOnBoard() : base(new ContainerRuntime())
    {

        this.Visual.Height = 100f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.Absolute;
        this.Visual.Width = 900f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.Absolute;

        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        ReactToGeneratedVisual();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        RejectQuestButton = new SupremeBroccoli.Components.Controls.ButtonStandard();
        RejectQuestButton.Name = "RejectQuestButton";
        AcceptQuestButton = new SupremeBroccoli.Components.Controls.ButtonStandard();
        AcceptQuestButton.Name = "AcceptQuestButton";
        QuestTitle = new global::Gum.GueDeriving.TextRuntime();
        QuestTitle.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (QuestTitle.ElementSave != null) QuestTitle.AddStatesAndCategoriesRecursivelyToGue(QuestTitle.ElementSave);
        if (QuestTitle.ElementSave != null) QuestTitle.SetInitialState();
        QuestTitle.Name = "QuestTitle";
        QuestDetails = new global::Gum.GueDeriving.TextRuntime();
        QuestDetails.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (QuestDetails.ElementSave != null) QuestDetails.AddStatesAndCategoriesRecursivelyToGue(QuestDetails.ElementSave);
        if (QuestDetails.ElementSave != null) QuestDetails.SetInitialState();
        QuestDetails.Name = "QuestDetails";
        StackPanelInstance = new SupremeBroccoli.Components.Controls.StackPanel();
        StackPanelInstance.Name = "StackPanelInstance";
    }
    protected virtual void InitializeInstancesFromVisual()
    {
        RejectQuestButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ButtonStandard>(this.Visual,"RejectQuestButton");
        AcceptQuestButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<ButtonStandard>(this.Visual,"AcceptQuestButton");
        QuestTitle = this.Visual?.GetGraphicalUiElementByName("QuestTitle") as global::Gum.GueDeriving.TextRuntime;
        QuestDetails = this.Visual?.GetGraphicalUiElementByName("QuestDetails") as global::Gum.GueDeriving.TextRuntime;
        StackPanelInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<StackPanel>(this.Visual,"StackPanelInstance");
    }
    protected virtual void AssignParents()
    {
        this.Visual.AddChild(this.RejectQuestButton.Visual);
        this.Visual.AddChild(this.AcceptQuestButton.Visual);
        this.Visual.AddChild(this.QuestTitle);
        this.Visual.AddChild(this.QuestDetails);
        this.Visual.AddChild(this.StackPanelInstance.Visual);
    }
    protected virtual void ApplyDefaultVariables()
    {
        this.RejectQuestButton.Text = @"Leave Quest";
        this.RejectQuestButton.Visual.X = 90f;
        this.RejectQuestButton.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.RejectQuestButton.Visual.Y = 10f;
        this.RejectQuestButton.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.AcceptQuestButton.Text = @"Accept Quest";
        this.AcceptQuestButton.Visual.X = 75f;
        this.AcceptQuestButton.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.AcceptQuestButton.Visual.Y = 10f;
        this.AcceptQuestButton.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.QuestTitle.Height = 10f;
        this.QuestTitle.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.QuestTitle.Text = @"Quest Title";
        this.QuestTitle.Width = 50f;
        this.QuestTitle.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.QuestTitle.X = 0f;
        this.QuestTitle.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.QuestTitle.Y = 10f;

        this.QuestDetails.Height = 50f;
        this.QuestDetails.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.QuestDetails.Text = @"Quest Details";
        this.QuestDetails.Width = 100f;
        this.QuestDetails.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.QuestDetails.Y = 50f;
        this.QuestDetails.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;


    }
    private bool _isGeneratedVisualBuilt;
    protected override void ReactToVisualChanged()
    {
        if (_isGeneratedVisualBuilt) base.ReactToVisualChanged();
    }
    private void ReactToGeneratedVisual()
    {
        _isGeneratedVisualBuilt = true;
        ReactToVisualChanged();
    }
    partial void CustomInitialize();
}
