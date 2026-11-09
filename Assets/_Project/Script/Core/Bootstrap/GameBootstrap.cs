using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private string firstScene = "Scn_World"; // Ubah ke "Scn_Title" jika sudah ada scene Title
    [SerializeField] private MonsterDatabase monsterDatabase;
    [SerializeField] private ScreenFader fader;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private AudioService audioService;
    [SerializeField] private ToastView toast;
    [SerializeField] private PauseMenu pauseMenu;

    private GameServices services;

    void Awake()
    {
        var actionInput = new InputSystemActionInput();
        var gate = new InputGate(new InputSystemMoveInput(), actionInput);

        services = new GameServices
        {
            Input = gate,
            RawAction = actionInput,
            Database = monsterDatabase,
            Party = new PlayerParty(),
            Battle = new BattleSession(),
            Flags = new StoryFlags(),
            Save = new SaveService(),
            Dialogue = dialogueRunner,
            Audio = audioService,
            Toast = toast,
        };
        services.Scenes = new SceneFlow(this, fader, services);
        dialogueRunner.Construct(gate, audioService);
        TextMenu.Sfx = audioService;
        pauseMenu.Construct(services);
    }

    void Start() => services.Scenes.GoTo(firstScene);

    void Update()
    {
        if (MenuInput.Mute) audioService.ToggleMute();
        if (services.Scenes.InGame) services.Party.Playtime += Time.unscaledDeltaTime;
    }
}