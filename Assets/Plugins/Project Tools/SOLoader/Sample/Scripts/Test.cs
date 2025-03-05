using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Tools.SOHelp
{
    public class Test : MonoBehaviour
    {
        public TextMeshProUGUI playerDataHealthText;
        public Button playerDataHealthIterate;
        public Button playerDataHealthDowngrade;
        public TextMeshProUGUI playerDataDamageText;
        public Button playerDataDamageIterate;
        public Button playerDataDamageDowngrade;
        public Button playerSaveManualButton;
        public TextMeshProUGUI playerSaveManualButtonText;
        public Button playerDeleteManualButton;

        public TextMeshProUGUI levelsDataIndexText;
        public Button levelsDataIndexIterate;
        public Button levelsDataIndexDowngrade;
        public TextMeshProUGUI levelsDataPositionText;
        public Button levelsDataPositionIterate;
        public Button levelsDataPositionDowngrade;
        public TextMeshProUGUI levelsDataRotationText;
        public Button levelsDataRotationIterate;
        public Button levelsDataRotationDowngrade;
        public TextMeshProUGUI levelsDataNameText;
        public TMP_InputField levelsDataNameInput;
        public Button levelsDataSetNameButton;
        public Button levelsSaveManualButton;
        public Button levelsFakeRequestButton;
        public TextMeshProUGUI levelsSaveManualButtonText;
        public Button levelsDeleteManualButton;

        public Image levelDataLogo;
        public Image levelDataColor;

        public Button exit;
        public Toggle saveOnUpdateToggle;

        private bool saveOnUpdate = false;
        private int levelDataPresetIndex = 1;
        private string autoSaveText = "Auto Save";

        private void Awake()
        {
            Setup();

            saveOnUpdateToggle.onValueChanged.AddListener((value) =>
            {
                saveOnUpdate = value;
            });
        }

        private void Update()
        {
            if (saveOnUpdate)
            {
                PlayerData.Value.health++;
                playerDataHealthText.text = PlayerData.Value.health.ToString();
                PlayerData.Value.damage++;
                playerDataDamageText.text = PlayerData.Value.damage.ToString();
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].index++;
                levelsDataIndexText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].index.ToString();
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].initialPosition += Vector3.one;
                levelsDataPositionText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].initialPosition.ToString();

                if (LevelsData.Value.IsAutoSaveAndSavable)
                {
                    LevelsData.Value.SaveData();
                }

                if (PlayerData.Value.IsAutoSaveAndSavable)
                {
                    PlayerData.Value.SaveData();
                }
            }
        }

        private void Setup()
        {
            playerDataHealthIterate.onClick.AddListener(() =>
            {
                PlayerData.Value.health++;
                UpdatePlayerDataUI();
            });

            playerDataHealthDowngrade.onClick.AddListener(() =>
            {
                PlayerData.Value.health--;
                UpdatePlayerDataUI();
            });

            playerDataDamageIterate.onClick.AddListener(() =>
            {
                PlayerData.Value.damage++;
                UpdatePlayerDataUI();
            });

            playerDataDamageDowngrade.onClick.AddListener(() =>
            {
                PlayerData.Value.damage--;
                UpdatePlayerDataUI();
            });

            if (PlayerData.Value.IsAutoSaveAndSavable)
            {
                playerSaveManualButton.interactable = false;
                playerSaveManualButtonText.text = autoSaveText;
            }
            else
            {
                playerSaveManualButton.onClick.AddListener(() =>
                {
                    PlayerData.Value.SaveData();
                });
            }

            playerDeleteManualButton.onClick.AddListener(() =>
            {
                PlayerData.Value.DeleteData();
                UpdatePlayerDataUI();
            });

            levelsDataIndexIterate.onClick.AddListener(() =>
            {
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].index++;
                UpdateLevelDataUI();
            });

            levelsDataIndexDowngrade.onClick.AddListener(() =>
            {
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].index--;
                UpdateLevelDataUI();
            });

            levelsDataPositionIterate.onClick.AddListener(() =>
            {
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].initialPosition += Vector3.one;
                UpdateLevelDataUI();
            });

            levelsDataPositionDowngrade.onClick.AddListener(() =>
            {
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].initialPosition -= Vector3.one;
                UpdateLevelDataUI();
            });

            levelsDataRotationIterate.onClick.AddListener(() =>
            {
                var rotation = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].rotation.eulerAngles;
                rotation += new Vector3(1f, 1f, 1f);
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].rotation = Quaternion.Euler(rotation);
                UpdateLevelDataUI();
            });

            levelsDataRotationDowngrade.onClick.AddListener(() =>
            {
                var rotation = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].rotation.eulerAngles;
                rotation -= new Vector3(1f, 1f, 1f);
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].rotation = Quaternion.Euler(rotation);
                UpdateLevelDataUI();
            });

            levelsDataSetNameButton.onClick.AddListener(() =>
            {
                LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].name = levelsDataNameInput.text;
                levelsDataNameInput.text = string.Empty;
                UpdateLevelDataUI();
            });

            if (LevelsData.Value.IsAutoSaveAndSavable)
            {
                levelsSaveManualButton.interactable = false;
                levelsSaveManualButtonText.text = autoSaveText;
            }
            else
            {
                levelsSaveManualButton.onClick.AddListener(() =>
                {
                    LevelsData.Value.SaveData();
                });
            }

            levelsFakeRequestButton.onClick.AddListener(() =>
            {
                GetJsonFromFakeRequestAndSetItToLevelData();
            });

            levelsDeleteManualButton.onClick.AddListener(() =>
            {
                LevelsData.Value.DeleteData();
                levelsDataNameInput.text = string.Empty;
                UpdateLevelDataUI();
            });

            exit.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            UpdatePlayerDataUI();
            UpdateLevelDataUI();
        }

        private void UpdatePlayerDataUI()
        {
            playerDataHealthText.text = PlayerData.Value.health.ToString();
            playerDataDamageText.text = PlayerData.Value.damage.ToString();
        }

        private void UpdateLevelDataUI()
        {
            levelsDataIndexText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].index.ToString();
            levelsDataNameText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].name;
            levelsDataPositionText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].initialPosition.ToString();
            levelsDataRotationText.text = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].rotation.ToString();

            levelDataLogo.sprite = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].logo;
            levelDataColor.color = LevelsData.Value.levelPresetsDictionary[levelDataPresetIndex].color;
        }

        private void GetJsonFromFakeRequestAndSetItToLevelData()
        {
            Debug.Log("Fake request is get from server");
            LevelsData.Value.SetValuesManual(FakeJson());
            UpdateLevelDataUI();
        }

        private string FakeJson()
        {
            var json = @"
        {
          ""levelPreset"": {
            ""index"": 15,
            ""name"": ""From fake json"",
            ""initialPosition"": {
              ""x"": 15.0,
              ""y"": 15.0,
              ""z"": 15.0
            },
            ""rotation"": {
              ""x"": 0.15,
              ""y"": 0.15,
              ""z"": 0.15,
              ""w"": 0.9998864
            }
          },
          ""levelPresets"": [
            {
              ""index"": 15,
              ""name"": ""From fake json"",
              ""initialPosition"": {
                ""x"": 15.0,
                ""y"": 15.0,
                ""z"": 15.0
              },
              ""rotation"": {
                ""x"": 0.15,
                ""y"": 0.15,
                ""z"": 0.15,
                ""w"": 0.9998864
              }
            },
            {
              ""index"": 15,
              ""name"": ""From fake json"",
              ""initialPosition"": {
                ""x"": 15.0,
                ""y"": 15.0,
                ""z"": 15.0
              },
              ""rotation"": {
                ""x"": 0.15,
                ""y"": 0.15,
                ""z"": 0.15,
                ""w"": 0.9995485
              }
            }
          ],
          ""levelPresetsDictionary"": {
            ""1"": {
              ""index"": 15,
              ""name"": ""From fake json"",
              ""initialPosition"": {
                ""x"": 15.0,
                ""y"": 15.0,
                ""z"": 15.0
              },
              ""rotation"": {
                ""x"": 0.15,
                ""y"": 0.15,
                ""z"": 0.15,
                ""w"": 0.9998864
              }
            },
            ""2"": {
              ""index"": 15,
              ""name"": ""From fake json"",
              ""initialPosition"": {
                ""x"": 15.0,
                ""y"": 15.0,
                ""z"": 15.0
              },
              ""rotation"": {
                ""x"": 0.15,
                ""y"": 0.15,
                ""z"": 0.15,
                ""w"": 0.9995485
              }
            }
          },
          ""name"": ""ProjectTools.SOHelp.LevelsData"",
          ""hideFlags"": 0
        }";

            return json;
        }
    }
}
