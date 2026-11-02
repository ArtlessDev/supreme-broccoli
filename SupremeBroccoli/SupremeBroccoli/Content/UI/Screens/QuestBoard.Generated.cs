//Code for QuestBoard
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using SupremeBroccoli.Components;
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
    public QuestOnBoard QuestOnBoardInstance3 { get; protected set; }
    public TextRuntime BoardTitle { get; protected set; }
    public ContainerRuntime BoardOfQuests { get; protected set; }
    public QuestOnBoard QuestOnBoardInstance { get; protected set; }
    public QuestOnBoard QuestOnBoardInstance1 { get; protected set; }
    public QuestOnBoard QuestOnBoardInstance2 { get; protected set; }

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
    protected virtual void InitializeInstances()
    {
        QuestOnBoardInstance3 = new SupremeBroccoli.Components.QuestOnBoard();
        QuestOnBoardInstance3.Name = "QuestOnBoardInstance3";
        BoardTitle = new global::MonoGameGum.GueDeriving.TextRuntime();
        BoardTitle.ElementSave = ObjectFinder.Self.GetStandardElement("Text");
        if (BoardTitle.ElementSave != null) BoardTitle.AddStatesAndCategoriesRecursivelyToGue(BoardTitle.ElementSave);
        if (BoardTitle.ElementSave != null) BoardTitle.SetInitialState();
        BoardTitle.Name = "BoardTitle";
        BoardOfQuests = new global::MonoGameGum.GueDeriving.ContainerRuntime();
        BoardOfQuests.ElementSave = ObjectFinder.Self.GetStandardElement("Container");
        if (BoardOfQuests.ElementSave != null) BoardOfQuests.AddStatesAndCategoriesRecursivelyToGue(BoardOfQuests.ElementSave);
        if (BoardOfQuests.ElementSave != null) BoardOfQuests.SetInitialState();
        BoardOfQuests.Name = "BoardOfQuests";
        QuestOnBoardInstance = new SupremeBroccoli.Components.QuestOnBoard();
        QuestOnBoardInstance.Name = "QuestOnBoardInstance";
        QuestOnBoardInstance1 = new SupremeBroccoli.Components.QuestOnBoard();
        QuestOnBoardInstance1.Name = "QuestOnBoardInstance1";
        QuestOnBoardInstance2 = new SupremeBroccoli.Components.QuestOnBoard();
        QuestOnBoardInstance2.Name = "QuestOnBoardInstance2";
        base.RefreshInternalVisualReferences();
    }
    protected virtual void AssignParents()
    {
        this.AddChild(QuestOnBoardInstance3);
        this.AddChild(BoardTitle);
        BoardTitle.AddChild(BoardOfQuests);
        BoardOfQuests.AddChild(QuestOnBoardInstance);
        BoardOfQuests.AddChild(QuestOnBoardInstance1);
        BoardOfQuests.AddChild(QuestOnBoardInstance2);
    }
    private void ApplyDefaultVariables()
    {
        this.Visual.Width = 0f;
        this.Visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.Visual.Height = 0f;
        this.Visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.QuestOnBoardInstance3.Visual.X = 93f;
        this.QuestOnBoardInstance3.Visual.Y = 444f;

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

        this.BoardOfQuests.ChildrenLayout = global::Gum.Managers.ChildrenLayout.TopToBottomStack;
        this.BoardOfQuests.Height = 200f;
        this.BoardOfQuests.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToMaxParentOrChildren;
        this.BoardOfQuests.Width = 900f;
        this.BoardOfQuests.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
        this.BoardOfQuests.WrapsChildren = false;
        this.BoardOfQuests.X = 0f;
        this.BoardOfQuests.Y = 0f;
        this.BoardOfQuests.YOrigin = global::RenderingLibrary.Graphics.VerticalAlignment.Top;
        this.BoardOfQuests.YUnits = global::Gum.Converters.GeneralUnitType.PixelsFromLarge;




    }
    partial void CustomInitialize();
}
