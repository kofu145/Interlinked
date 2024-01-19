using System.Data;
using System.Drawing;
using System.Numerics;
using DialogueTesting.Components;
using GramEngine.Core.Input;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using Interlinked.Components;
using Transform = SFML.Graphics.Transform;

namespace Interlinked;
using GramEngine.Core;

public class Overworld : GameState
{
    private Entity playerEntity;
    private Entity thalrogg;
    private Entity celia;
    private Entity bethard;
    public const float npcSize = 5f;
    private Entity owbg;
    public override void Initialize()
    {
        base.Initialize();
        GameStateManager.Window.BackgroundColor = Color.FromArgb(139, 180, 141);
        GameStateManager.Window.Zoom = 200f;
        
        playerEntity = new Entity();
        playerEntity.Tag = "player";
        playerEntity.AddComponent(new Sprite("Content/nyplayer.png"));
        playerEntity.AddComponent(new Player(2000f));
        playerEntity.Transform.Position =
            new Vector3(3000, 
                3000, 5);
        playerEntity.AddComponent(new Animation());
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayeridle-Sheet.png", "idle", .4f, (16, 16));
        playerEntity.GetComponent<Animation>().LoadTextureAtlas("Content/nyplayerrun-Sheet.png", "running", .13f, (16, 16));
        playerEntity.GetComponent<Animation>().SetState("idle");
        playerEntity.AddComponent(new CircleCollider(30, true, true));

        playerEntity.GetComponent<CircleCollider>().OnCollision += (CircleCollider other) =>
        {
            if (other.ParentEntity.Tag == "NPC" && !playerEntity.GetComponent<Player>().Talking
                && !other.ParentEntity.GetComponent<ConversationManager>().talkedFlag)
            {
                if (InputManager.GetKeyPressed(Keys.Enter))
                {
                    playerEntity.GetComponent<Player>().Talking = true;
                    other.ParentEntity.GetComponent<ConversationManager>().StartDialogue();

                }
            }
        };
        
        thalrogg = new Entity();
        thalrogg.AddComponent(new Sprite("Content/nyplayer.png"));
        thalrogg.AddComponent(new CircleCollider(80, true));
        thalrogg.Transform.Scale = new Vector2(npcSize, npcSize);
        thalrogg.Transform.Position = new Vector3(-1580, 2110, 0);
        thalrogg.AddComponent(new Animation());
        thalrogg.GetComponent<Animation>().LoadTextureAtlas("Content/thalrogg-Sheet.png", "idle", .4f, (16, 16));
        thalrogg.GetComponent<Animation>().SetState("idle");
        thalrogg.AddComponent(new ConversationManager("Content/TDialogue/test.txt", "Content/TDialogue/finished.txt", "thalrogg"));
        thalrogg.Tag = "NPC";

        celia = new Entity();
        celia.AddComponent(new Sprite("Content/nyplayer.png"));
        celia.AddComponent(new CircleCollider(80, true));
        celia.Transform.Scale = new Vector2(npcSize, npcSize);
        celia.Transform.Position = new Vector3(2740, 2024,1);
        celia.AddComponent(new Animation());
        celia.GetComponent<Animation>().LoadTextureAtlas("Content/celianohat-Sheet.png", "idle", .4f, (16,16));
        celia.GetComponent<Animation>().SetState("idle");
        celia.AddComponent(new ConversationManager("Content/CDialogue/test.txt", "Content/CDialogue/finished.txt", "Celia"));
        celia.Tag = "NPC";

        bethard = new Entity();
        bethard.AddComponent(new Sprite("Content/nyplayer.png"));
        bethard.AddComponent(new CircleCollider(80, true));
        bethard.Transform.Scale = new Vector2(npcSize, npcSize);
        bethard.Transform.Position = new Vector3(3391, 219, 0);
        bethard.AddComponent(new Animation());
        bethard.GetComponent<Animation>().LoadTextureAtlas("Content/bethard-Sheet.png", "idle", .4f, (16, 16));
        bethard.GetComponent<Animation>().SetState("idle");
        bethard.AddComponent(new ConversationManager("Content/BDialogue/test.txt", "Content/BDialogue/finished.txt", "Bethard"));
        bethard.Tag = "NPC";
        
        owbg = new Entity();
        owbg.AddComponent(new Sprite("Content/testow.png"));
        owbg.Transform.Scale = new Vector2(8, 8);
        owbg.Transform.Position = new Vector3(GameStateManager.Window.Width / 2, 
                GameStateManager.Window.Height / 2, -1);
        var prev = 0;
        for (int i = 0; i < Constants.rocks.Length; i++)
        {
            // 
            if (i % 2 == 0)
            {
                prev = Constants.rocks[i];
            }
            else
            {
                var circle = new Entity();
                circle.AddComponent(new CircleCollider(200, false));
                circle.Transform.Position = new Vector3(prev, Constants.rocks[i], 0);
                AddEntity(circle);
            }
        }
        for (int i = 0; i < Constants.tree.Length; i++)
        {
            if (i % 2 == 0)
            {
                prev = Constants.tree[i];
            }
            else
            {
                var circle = new Entity();
                circle.AddComponent(new CircleCollider(200, false));
                circle.Transform.Position = new Vector3(prev, Constants.tree[i], 0);
                AddEntity(circle);
            }
        }
        
        for (int i = 0; i < Constants.bigtree.Length; i++)
        {
            if (i % 2 == 0)
            {
                prev = Constants.bigtree[i];
            }
            else
            {
                var circle = new Entity();
                circle.AddComponent(new CircleCollider(500, false));
                circle.Transform.Position = new Vector3(prev, Constants.bigtree[i], 0);
                AddEntity(circle);
            }
        }
        for (int i = 0; i < Constants.lake.Length; i++)
        {
            if (i % 2 == 0)
            {
                prev = Constants.lake[i];
            }
            else
            {
                var circle = new Entity();
                circle.AddComponent(new CircleCollider(600, false));
                circle.Transform.Position = new Vector3(prev, Constants.lake[i], 0);
                AddEntity(circle);
            }
        }

        AddEntity(playerEntity);
        AddEntity(thalrogg);
        AddEntity(celia);
        AddEntity(bethard);
        AddEntity(owbg);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var CameraPos = new Vector2(
            playerEntity.Transform.Position.X - GameStateManager.Window.settings.Width / 2,
            playerEntity.Transform.Position.Y - GameStateManager.Window.settings.Height / 2);
        GameStateManager.Window.CameraPosition = CameraPos;
        Console.WriteLine(playerEntity.Transform.Position);
        
    }
    
    
    

}