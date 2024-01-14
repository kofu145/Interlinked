using System.Drawing;
using System.Numerics;
using GramEngine.Core;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using GramEngine.ECS.Components.UI;

namespace Interlinked;

public class TitleScreen : GameState
{
    private Entity background;
    private int speed = 50;
    public override void Initialize()
    {
        base.Initialize();
        var title = new Entity();
        title.AddComponent(new Sprite("Content/title.png"));
        title.Transform.Position = new Vector3(GameStateManager.Window.settings.Width / 2, 200, 1);
        title.Transform.Scale = new Vector2(3f, 3f);
        title.AddComponent(new Sound("Content/kf-menu.wav", "title"));
        title.GetComponent<Sound>().Play();
        
        background = new Entity();
        background.AddComponent(new Sprite("Content/blurredow.png"));
        background.Transform.Position = new Vector3(GameStateManager.Window.settings.Width / 2, 
            GameStateManager.Window.settings.Height / 2, 0);
        background.Transform.Scale = new Vector2(5f, 5f);
        var button = new Entity();
        button.AddComponent(new Button(300, 100));
        button.AddComponent(new TextComponent("START", "SourceFiles/square.ttf", 80));
        button.AddComponent(new RenderRect(new Vector2(600, 100)));
        var rect = button.GetComponent<RenderRect>();
        rect.FillColor = Color.FromArgb(247, 239, 199);
        rect.BorderThickness = 4;
        rect.OutlineColor = Color.FromArgb(25, 31, 34);
        button.Transform.Position =  new Vector3(GameStateManager.Window.settings.Width / 2 - rect.Size.X / 2, 500, 1);
        button.GetComponent<TextComponent>().TextOffset = new Vector2(180, 0);
        button.GetComponent<TextComponent>().FillColor =  Color.FromArgb(25, 31, 34);
        button.GetComponent<Button>().OnButtonDown += () =>
        {
            title.GetComponent<Sound>().Dispose();
            GameStateManager.AddScreen(new Overworld());
        };

        AddEntity(button);
        AddEntity(title);
        AddEntity(background);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        background.Transform.Position.X += speed * gameTime.DeltaTime;
        if (background.Transform.Position.X <= 0 ||
            background.Transform.Position.X >= GameStateManager.Window.settings.Width)
        {
            speed = -speed;
        }
    }
}