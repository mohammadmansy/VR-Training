using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimpleStepManager : MonoBehaviour
{
    public static SimpleStepManager Instance;

    [System.Serializable]
    public class Step
    {
        public string stepName = "مرحلة جديدة";
        public AudioClip audioClip;
        public Sprite stepSprite;

        public UnityEvent onAudioStart;
        public UnityEvent onAudioEnd;
        public UnityEvent onStepComplete;
    }

    [Header("Steps Setup")]
    public List<Step> steps = new List<Step>();

    [Header("References")]
    public AudioSource audioSource;
    public Image stepImage;

    private int currentStep = 0;
    private bool audioPlaying = false;
    private bool isStarted = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (isStarted && audioPlaying && !audioSource.isPlaying)
        {
            audioPlaying = false;
            steps[currentStep].onAudioEnd?.Invoke();

            if (currentStep == 0)
            {
                Invoke("NextStep", 2f);
            }
        }
    }

    void StartStep(int stepIndex)
    {
        if (stepIndex < 0 || stepIndex >= steps.Count)
        {
            return;
        }

        currentStep = stepIndex;
        Step step = steps[currentStep];


        if (stepImage != null && step.stepSprite != null)
        {
            stepImage.sprite = step.stepSprite;
        }

        if (step.audioClip != null && audioSource != null)
        {
            audioSource.clip = step.audioClip;
            audioSource.Play();
            audioPlaying = true;
            step.onAudioStart?.Invoke();
        }
        else
        {
            step.onAudioStart?.Invoke();
        }
    }

    public void NextStep()
    {
        steps[currentStep].onStepComplete?.Invoke();

        currentStep++;
        if (currentStep < steps.Count)
        {
            StartStep(currentStep);
        }
        else
        {
            Debug.Log("🎉 خلصنا كل الخطوات!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isStarted && other.CompareTag("Player"))
        {
            isStarted = true;
            Debug.Log("🚀 اللاعب دخل عند الـ Instructor → ابدأ Step 0");
            StartStep(0);
        }
    }
}
