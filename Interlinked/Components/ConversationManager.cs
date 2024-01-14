using System.Drawing;
using System.Numerics;
using DialogueTesting.Prefabs;
using GramEngine.Core;
using GramEngine.ECS;
using GramEngine.ECS.Components;
using GramEngine.ECS.Components.UI;
using Interlinked.Components;

namespace DialogueTesting.Components;

public class ConversationManager : Component
{
    private string finishedPath;
    private string convName;
    private bool conversing;
    private Dialogue textGenerator;
    private Entity boxEntity;
    private DialogueManager dialogueManager;
    private string[] options;
    private List<string> mainFile;
    public bool talkedFlag;
    private float timer;
    private bool useFinish;
    private Entity player;
    private string name;

    public ConversationManager(string conversationPath, string finishedPath, string name)
    {
        this.finishedPath = finishedPath;
        convName = conversationPath.Split(".")[0];
        conversing = false;
        talkedFlag = false;
        useFinish = false;
        options = new string[2];
        mainFile = new List<string>();
        timer = 0;
        this.name = name;

    }

    public override void Initialize()
    {
        player = ParentScene.FindWithTag("player");
    }

    public void StartDialogue()
    {
        if (!useFinish)
        {
            InvokeText(convName + ".txt");
        }
        else
        {
            InvokeText(finishedPath);
        }
    }

    private void InvokeText(string name)
    {
        mainFile.Clear();
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = "none";
        }
        
        var readFile = File.ReadAllLines(name);
        for (int i=0; i<readFile.Length; i++)
        {
            if (readFile[i].StartsWith("option:"))
            {
                options[i] = readFile[i].Split(":")[1];
            }
            else
            {
                mainFile.Add(readFile[i]);
            }
        }
        
        textGenerator = new Dialogue(mainFile.ToArray(), ParentScene.FindWithTag("player").Transform, this.name);
        boxEntity = textGenerator.Instantiate();
        dialogueManager = boxEntity.GetComponent<DialogueManager>();
        ParentScene.AddEntity(boxEntity);
        conversing = true;

    }

    private void CreateDecisions(GameTime gameTime)
    {
        if (options[0] == "none")
        {
            talkedFlag = true;
            useFinish = true;
            timer = (float)gameTime.TotalTime.TotalSeconds + .5f;
            ParentScene.DestroyEntity(boxEntity);
            player.GetComponent<Player>().Talking = false;
            return;
        }

        var buttonsList = new Entity[2];
        // make the buttons for conversation\
        for (int i=0; i<options.Length; i++)
        {
            var button = new Entity();
            button.AddComponent(new Button(1000, 70));
            var buttComp = button.GetComponent<Button>();
            var playerPos = ParentScene.FindWithTag("player").Transform.Position;
            button.Transform.Position = new Vector3(
                (float)playerPos.X - buttComp.Width / 2,
                (playerPos.Y - (float)GameStateManager.Window.settings.Height / 4) * (i + 1) + 100,
                100f);
            button.AddComponent(new RenderRect(new Vector2(1000, 70)));
            var rect = button.GetComponent<RenderRect>();
            rect.FillColor = Color.FromArgb(89, 82, 70);
            rect.BorderThickness = 7;
            rect.OutlineColor = Color.FromArgb(25, 31, 34);
            button.AddComponent(new TextComponent(options[i], "SourceFiles/square.ttf", 30));
            button.GetComponent<TextComponent>().TextOffset = new Vector2( buttComp.Width*.03f, buttComp.Height*.2f);

            var captureValue = i+1;
            buttComp.OnButtonDown += () =>
            {
                convName += $"_{captureValue}";
                //after decisions have been made
                ParentScene.DestroyEntity(boxEntity);
                foreach (var butt in buttonsList)
                {
                    ParentScene.DestroyEntity(butt);
                }
                InvokeText(convName + ".txt");
            };
            buttonsList[i] = button;
            ParentScene.AddEntity(button);

        }
        
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (conversing && boxEntity != null)
        {
            if (dialogueManager.Finished)
            {
                CreateDecisions(gameTime);
                conversing = false;
            }
        }

        if (talkedFlag && !ParentScene.FindWithTag("player").GetComponent<Player>().Talking &&
            gameTime.TotalTime.TotalSeconds > timer)
        {
            talkedFlag = false;
        }
        
    }
    
}