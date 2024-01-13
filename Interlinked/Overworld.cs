using System.Drawing;
using System.Numerics;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using Interlinked.Components;
using Transform = SFML.Graphics.Transform;

namespace Interlinked;
using GramEngine.Core;

public class Overworld : GameState
{
    private Entity playerEntity;
    private Entity owbg;
    public override void Initialize()
    {
        base.Initialize();
        GameStateManager.Window.BackgroundColor = Color.FromArgb(139, 180, 141);
        GameStateManager.Window.Zoom = 200f;
        
        playerEntity = new Entity();
        playerEntity.AddComponent(new Sprite("Content/nyplayer.png"));
        playerEntity.AddComponent(new Player(300f));
        playerEntity.Transform.Position =
            new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, 0);
        playerEntity.AddComponent(new Animation());
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayeridle-Sheet.png", "idle", .4f, (16, 16));
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayerrun-Sheet.png", "running", .13f, (16, 16));
        playerEntity.GetComponent<Animation>().SetState("idle");
        playerEntity.AddComponent(new CircleCollider(30, true));

        owbg = new Entity();
        owbg.AddComponent(new Sprite("Content/testow.png"));
        owbg.Transform.Scale = new Vector2(10, 10);
        owbg.Transform.Position = new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, -1);
        
        var circ = new Entity();
        circ.AddComponent(new CircleCollider(50, true));
        var circ2 = new Entity();
        circ2.AddComponent(new CircleCollider(50, false, false));
        circ2.Transform.Position = new Vector3(30, 30, 0);

        for (int i = 0; i < 200; i++)
        {
            var col = new Entity();
            col.AddComponent(new CircleCollider(50, false));
            var rand = new Random();
            col.Transform.Position = new Vector3(rand.Next(500), rand.Next(500), 0);
            AddEntity(col);
        }
        
        AddEntity(circ); 
        AddEntity(circ2);
        AddEntity(playerEntity);
        AddEntity(owbg);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var CameraPos = new Vector2(
            playerEntity.Transform.Position.X - GameStateManager.Window.settings.Width / 2,
            playerEntity.Transform.Position.Y - GameStateManager.Window.settings.Height / 2);
        GameStateManager.Window.CameraPosition = CameraPos;

        
        
    }
}