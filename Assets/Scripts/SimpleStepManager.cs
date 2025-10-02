using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SimpleStepManager : MonoBehaviour
{
    public static SimpleStepManager Instance;

    [System.Serializable]
    public class Step
    {
        public string stepName = "مرحلة جديدة";
        public AudioClip audioClip;

        [Header("أحداث")]
        public UnityEvent onAudioStart;
        public UnityEvent onAudioEnd;
        public UnityEvent onStepComplete;
    }

    public List<Step> steps = new List<Step>();
    public AudioSource audioSource;

    private int currentStep = 0;
    private bool audioPlaying = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (steps.Count > 0)
            StartStep(0);
    }

    void Update()
    {
        if (audioPlaying && !audioSource.isPlaying)
        {
            audioPlaying = false;
            steps[currentStep].onAudioEnd?.Invoke();

            // Step 0 ينقل تلقائي
            if (currentStep == 0)
            {
                Invoke("NextStep", 2f); // بعد ثانيتين
            }
        }
    }
    void StartStep(int stepIndex)
    {
        currentStep = stepIndex;
        Step step = steps[currentStep];

        Debug.Log("▶️ بدأ Step: " + step.stepName);

        if (step.audioClip != null)
        {
            audioSource.clip = step.audioClip;
            audioSource.Play();
            audioPlaying = true;
            step.onAudioStart?.Invoke();
        }
    }

    public void NextStep()
    {
        steps[currentStep].onStepComplete?.Invoke();

        currentStep++;
        if (currentStep < steps.Count)
        {
            Invoke("StartStep", 1f);
            StartStep(currentStep);
        }
        else
        {
            Debug.Log("🎉 خلصنا!");
        }
    }
}