using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;   // <-- عشان UI Image
using System.Collections.Generic;

public class SimpleStepManager : MonoBehaviour
{
    public static SimpleStepManager Instance;

    [System.Serializable]
    public class Step
    {
        public string stepName = "مرحلة جديدة";
        public AudioClip audioClip;
        public Sprite stepSprite; // 👈 الصورة الخاصة بالخطوة

        [Header("أحداث")]
        public UnityEvent onAudioStart;
        public UnityEvent onAudioEnd;
        public UnityEvent onStepComplete;
    }

    [Header("الخطوات")]
    public List<Step> steps = new List<Step>();

    [Header("مكونات")]
    public AudioSource audioSource;
    public Image stepImage;  // 👈 الصورة في الـ UI (من Canvas)

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

            // مثال: الخطوة 0 تنقل تلقائي
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

        // 🔹 تغيير الصورة في UI
        if (stepImage != null && step.stepSprite != null)
        {
            stepImage.sprite = step.stepSprite;
        }

        // 🔹 تشغيل الصوت
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
            StartStep(currentStep);
        }
        else
        {
            Debug.Log("🎉 خلصنا كل الخطوات!");
        }
    }
}
