using GramEngine.Core;
using Interlinked;

WindowSettings windowSettings = new WindowSettings()
{
    NaiveCollision = true,
    WindowTitle = "Interlinked",
    Width = 1280,
    Height = 760,
    ShowColliders = true,
    ShowFPS = false
};

Window window = new Window(new TitleScreen(), windowSettings);

window.Run();