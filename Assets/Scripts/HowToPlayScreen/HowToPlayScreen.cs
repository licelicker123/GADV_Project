using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HowToPlayScreen : MonoBehaviour
{
    [Header("Pages (add in order)")]
    public List<GameObject> pages;

    [Header("Buttons")]
    public Button nextButton;
    public Button prevButton;
    public Button startGameButton;

    private int currentPageIndex = 0;

    // debounce to ignore duplicate calls within a short interval
    private float lastClickTime = -1f;
    private const float clickDebounceSeconds = 0.25f;

    private void Awake()
    {
        // Diagnostic: how many copies of this script exist?
        var copies = FindObjectsOfType<HowToPlayScreen>();
        if (copies.Length > 1)
        {
            Debug.LogWarning($"[HowToPlayScreen] Warning: {copies.Length} instances found in scene. That can cause duplicate calls.");
        }
        else
        {
            Debug.Log($"[HowToPlayScreen] Instances found: {copies.Length}");
        }

        // Diagnostic: how many persistent listeners in the Inspector (if any)
        if (nextButton != null)
            Debug.Log($"[HowToPlayScreen] nextButton persistent listeners count: {nextButton.onClick.GetPersistentEventCount()}");
        if (prevButton != null)
            Debug.Log($"[HowToPlayScreen] prevButton persistent listeners count: {prevButton.onClick.GetPersistentEventCount()}");
    }

    private void OnEnable()
    {
        // Always remove runtime listeners then add fresh ones to avoid duplicates
        if (nextButton != null && prevButton != null)
        {
            nextButton.onClick.RemoveAllListeners();
            prevButton.onClick.RemoveAllListeners();
            startGameButton.onClick.RemoveAllListeners();

            nextButton.onClick.AddListener(NextPage);
            prevButton.onClick.AddListener(PreviousPage);
            startGameButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("[HowToPlayScreen] nextButton or prevButton not assigned in Inspector!");
        }

        currentPageIndex = 0;
        ShowPage(currentPageIndex);
    }

    private void OnDisable()
    {
        // Clean up listeners when disabled (extra safety)
        if (nextButton != null) nextButton.onClick.RemoveListener(NextPage);
        if (prevButton != null) prevButton.onClick.RemoveListener(PreviousPage);
    }

    public void NextPage()
    {
        // Diagnostic stack trace to see who calls it (helps identify duplicate callers)
        Debug.Log("[HowToPlayScreen] NextPage called\n" + new System.Diagnostics.StackTrace(true).ToString());

        // Debounce: ignore extra calls within short time window
        if (Time.unscaledTime - lastClickTime < clickDebounceSeconds)
        {
            Debug.Log("[HowToPlayScreen] NextPage ignored due to debounce (duplicate).");
            return;
        }
        lastClickTime = Time.unscaledTime;

        if (pages == null || pages.Count == 0)
        {
            Debug.LogError("[HowToPlayScreen] Pages list empty or unassigned.");
            return;
        }

        if (currentPageIndex < pages.Count - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    public void PreviousPage()
    {
        Debug.Log("[HowToPlayScreen] PreviousPage called\n" + new System.Diagnostics.StackTrace(true).ToString());

        if (Time.unscaledTime - lastClickTime < clickDebounceSeconds)
        {
            Debug.Log("[HowToPlayScreen] PreviousPage ignored due to debounce (duplicate).");
            return;
        }
        lastClickTime = Time.unscaledTime;

        if (pages == null || pages.Count == 0)
        {
            Debug.LogError("[HowToPlayScreen] Pages list empty or unassigned.");
            return;
        }

        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }

    private void ShowPage(int index)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i] != null)
                pages[i].SetActive(i == index);
        }

        if (prevButton != null) prevButton.interactable = index > 0;
        if (nextButton != null) nextButton.interactable = index < pages.Count - 1;
        startGameButton.gameObject.SetActive(index == pages.Count - 1);
    }
    private void StartGame()
    {
        // Replace "GameScene" with your actual scene name
        SceneManager.LoadScene("Game");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu"); //press button to go main menu
    }
}
