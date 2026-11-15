//Code for DialogueScreen
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
[global::Gum.DataTypes.GumElement("DialogueScreen")]
partial class DialogueScreen : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("DialogueScreen");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new DialogueScreen(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(DialogueScreen)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("DialogueScreen", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public DialogBox DialogueBox { get; protected set; }
    public NameplateComponent NameplateComponentInstance { get; protected set; }

    public DialogueScreen(InteractiveGue visual) : base(visual)
    {
        InitializeInstancesFromVisual();
        CustomInitialize();
    }
    public DialogueScreen() : base(new ContainerRuntime())
    {


        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        DialogueBox = new SupremeBroccoli.Components.Controls.DialogBox();
        DialogueBox.Name = "DialogueBox";
        NameplateComponentInstance = new SupremeBroccoli.Components.NameplateComponent();
        NameplateComponentInstance.Name = "NameplateComponentInstance";
    }
    protected virtual void InitializeInstancesFromVisual()
    {
        DialogueBox = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<DialogBox>(this.Visual,"DialogueBox");
        NameplateComponentInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.FindFormsControl<NameplateComponent>(this.Visual,"NameplateComponentInstance");
    }
    protected virtual void AssignParents()
    {
        this.AddChild(DialogueBox);
        this.AddChild(NameplateComponentInstance);
    }
    protected virtual void ApplyDefaultVariables()
    {
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.DialogueBox.Visual.Height = 15f;
        this.DialogueBox.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.DialogueBox.Visual.Width = 80f;
        this.DialogueBox.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.DialogueBox.Visual.X = 50f;
        this.DialogueBox.Visual.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Center;
        this.DialogueBox.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.DialogueBox.Visual.Y = 95f;
        this.DialogueBox.Visual.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Bottom;
        this.DialogueBox.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.NameplateComponentInstance.Visual.X = 10f;
        this.NameplateComponentInstance.Visual.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.NameplateComponentInstance.Visual.Y = 75f;
        this.NameplateComponentInstance.Visual.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

    }
    partial void CustomInitialize();
}
