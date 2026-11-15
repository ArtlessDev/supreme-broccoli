//Code for NameplateComponent (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace SupremeBroccoli.Components;
[global::Gum.DataTypes.GumElement("NameplateComponent")]
partial class NameplateComponent : global::Gum.Forms.Controls.FrameworkElement
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
            var element = ObjectFinder.Self.GetRequiredElementSave("NameplateComponent");
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new NameplateComponent(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(NameplateComponent)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("NameplateComponent", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public NineSliceRuntime NineSliceInstance { get; protected set; }
    public TextRuntime SpeakerNamePlate { get; protected set; }

    public NameplateComponent(InteractiveGue visual) : base(visual)
    {
        InitializeInstancesFromVisual();
        ReactToGeneratedVisual();
        CustomInitialize();
    }
    public NameplateComponent() : base(new ContainerRuntime())
    {

        this.Visual.ChildrenLayout = global::Gum.Managers.ChildrenLayout.Regular;
        this.Visual.ClipsChildren = false;
        this.Visual.Height = 50f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.Visual.Width = 10f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.Visual.WrapsChildren = false;

        InitializeInstances();

        ApplyDefaultVariables();
        AssignParents();
        ReactToGeneratedVisual();
        CustomInitialize();
    }
    protected virtual void InitializeInstances()
    {
        NineSliceInstance = new global::Gum.GueDeriving.NineSliceRuntime();
        NineSliceInstance.ElementSave = ObjectFinder.Self.GetStandardElement("NineSlice");
        if (NineSliceInstance.ElementSave != null) NineSliceInstance.AddStatesAndCategoriesRecursivelyToGue(NineSliceInstance.ElementSave);
        if (NineSliceInstance.ElementSave != null) NineSliceInstance.SetInitialState();
        NineSliceInstance.Name = "NineSliceInstance";
        SpeakerNamePlate = new global::Gum.GueDeriving.TextRuntime();
        SpeakerNamePlate.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (SpeakerNamePlate.ElementSave != null) SpeakerNamePlate.AddStatesAndCategoriesRecursivelyToGue(SpeakerNamePlate.ElementSave);
        if (SpeakerNamePlate.ElementSave != null) SpeakerNamePlate.SetInitialState();
        SpeakerNamePlate.Name = "SpeakerNamePlate";
    }
    protected virtual void InitializeInstancesFromVisual()
    {
        NineSliceInstance = this.Visual?.GetGraphicalUiElementByName("NineSliceInstance") as global::Gum.GueDeriving.NineSliceRuntime;
        SpeakerNamePlate = this.Visual?.GetGraphicalUiElementByName("SpeakerNamePlate") as global::Gum.GueDeriving.TextRuntime;
    }
    protected virtual void AssignParents()
    {
        this.Visual.AddChild(this.NineSliceInstance);
        this.Visual.AddChild(this.SpeakerNamePlate);
    }
    protected virtual void ApplyDefaultVariables()
    {
        this.NineSliceInstance.Height = 100f;
        this.NineSliceInstance.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.NineSliceInstance.SourceFileName = @"KenneyAssets\panel_brown.png";
        this.NineSliceInstance.Width = 100f;
        this.NineSliceInstance.WidthUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        this.NineSliceInstance.X = 0f;
        this.NineSliceInstance.XOrigin = global::RenderingLibrary.Graphics.HorizontalAlignment.Left;
        this.NineSliceInstance.XUnits = global::Gum.Converters.GeneralUnitType.Percentage;
        this.NineSliceInstance.Y = 0f;
        this.NineSliceInstance.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.NineSliceInstance.YUnits = global::Gum.Converters.GeneralUnitType.Percentage;

        this.SpeakerNamePlate.Font = @"C:\Code\supreme-broccoli\SupremeBroccoli\SupremeBroccoli\Content\Fonts\KGShadowOfTheDay.ttf";
        this.SpeakerNamePlate.Height = 100f;
        this.SpeakerNamePlate.HeightUnits = global::Gum.DataTypes.DimensionUnitType.PercentageOfParent;
        ((TextRuntime)this.SpeakerNamePlate).HorizontalAlignment = global::RenderingLibrary.Graphics.HorizontalAlignment.Right;
        this.SpeakerNamePlate.IsItalic = false;
        this.SpeakerNamePlate.Text = @"pannetone is a silly little kitty and she loves her churu";
        this.SpeakerNamePlate.TextOverflowVerticalMode = global::RenderingLibrary.Graphics.TextOverflowVerticalMode.SpillOver;
        this.SpeakerNamePlate.UseCustomFont = false;
        ((TextRuntime)this.SpeakerNamePlate).VerticalAlignment = global::RenderingLibrary.Graphics.VerticalAlignment.Center;
        this.SpeakerNamePlate.Width = 10f;
        this.SpeakerNamePlate.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        this.SpeakerNamePlate.X = 0f;
        this.SpeakerNamePlate.XUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;
        this.SpeakerNamePlate.Y = 0f;
        this.SpeakerNamePlate.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.SpeakerNamePlate.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromSmall;

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
