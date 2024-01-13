using GramEngine.Core;
using Interlinked;

WindowSettings windowSettings = new WindowSettings()
{
    NaiveCollision = true,
    WindowTitle = "Interlinked",
    Width = 1280,
    Height = 760
};

Window window = new Window(new Overworld(), windowSettings);

window.Run();