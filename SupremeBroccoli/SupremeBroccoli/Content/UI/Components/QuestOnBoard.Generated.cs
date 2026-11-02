//Code for QuestOnBoard (Container)
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
namespace SupremeBroccoli.Components;
partial class QuestOnBoard : global::Gum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("QuestOnBoard") ?? throw new System.InvalidOperationException("Could not find an element named QuestOnBoard - did you forget to load a Gum project?");
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

    public QuestOnBoard(InteractiveGue visual) : base(visual)
    {
        InitializeInstances();
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
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        RejectQuestButton = new SupremeBroccoli.Components.Controls.ButtonStandard();
        RejectQuestButton.Name = "RejectQuestButton";
        AcceptQuestButton = new SupremeBroccoli.Components.Controls.ButtonStandard();
        AcceptQuestButton.Name = "AcceptQuestButton";
        QuestTitle = new global::MonoGameGum.GueDeriving.TextRuntime();
        QuestTitle.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (QuestTitle.ElementSave != null) QuestTitle.AddStatesAndCategoriesRecursivelyToGue(QuestTitle.ElementSave);
        if (QuestTitle.ElementSave != null) QuestTitle.SetInitialState();
        QuestTitle.Name = "QuestTitle";
        QuestDetails = new global::MonoGameGum.GueDeriving.TextRuntime();
        QuestDetails.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (QuestDetails.ElementSave != null) QuestDetails.AddStatesAndCategoriesRecursivelyToGue(QuestDetails.ElementSave);
        if (QuestDetails.ElementSave != null) QuestDetails.SetInitialState();
        QuestDetails.Name = "QuestDetails";
        base.RefreshInternalVisualReferences();
    }
    protected virtual void AssignParents()
    {
        this.AddChild(RejectQuestButton);
        this.AddChild(AcceptQuestButton);
        this.AddChild(QuestTitle);
        this.AddChild(QuestDetails);
    }
    private void ApplyDefaultVariables()
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
    partial void CustomInitialize();
}
