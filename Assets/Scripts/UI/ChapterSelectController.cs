using TMPro;
using UnityEngine;

public class ChapterSelectController : MonoBehaviour
{
    [System.Serializable]
    public class Chapter
    {
        public string title;

        [TextArea]
        public string description;

        public int strikeTarget;
        public float timeLimit;
        public int reward;
    }

    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject briefingScreen;
    [SerializeField] TMP_Text missionTitle;
    [SerializeField] TMP_Text missionDetails;

    [SerializeField]
    Chapter[] chapters =
    {
        new Chapter
        {
            title = "The Bamboo Path",
            description =
                "Follow the bamboo trail and practise your first strikes.",
            strikeTarget = 5,
            timeLimit = 20f,
            reward = 60
        },
        new Chapter
        {
            title = "The Shrine Gate",
            description =
                "Clear a path through the guarded entrance to the old shrine.",
            strikeTarget = 7,
            timeLimit = 22f,
            reward = 90
        },
        new Chapter
        {
            title = "The Mountain Pass",
            description =
                "Hold your ground on the mountain route and finish the encounter.",
            strikeTarget = 10,
            timeLimit = 25f,
            reward = 120
        }
    };

    public int SelectedIndex { get; private set; }
    public Chapter SelectedChapter => chapters[SelectedIndex];

    void Start()
    {
        RefreshBriefing();
    }

    public void SelectChapter(int index)
    {
        if (uiManager.IsTransitioning ||
            index < 0 || index >= chapters.Length)
        {
            return;
        }

        SelectedIndex = index;
        RefreshBriefing();
        uiManager.OpenScreen(briefingScreen);
    }

    void RefreshBriefing()
    {
        Chapter chapter = SelectedChapter;

        missionTitle.text = chapter.title;

        missionDetails.text =
            $"{chapter.description}\n\n" +
            $"Objective: Land {chapter.strikeTarget} strikes.\n" +
            $"Time limit: {chapter.timeLimit:0} seconds\n" +
            $"Reward: {chapter.reward} coins";
    }
}