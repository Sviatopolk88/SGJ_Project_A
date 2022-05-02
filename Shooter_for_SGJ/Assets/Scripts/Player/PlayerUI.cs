using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp.Assets.Scripts.Player
{

    public class PlayerUI : MonoBehaviour
    {
        
        public bool GameIsOver = false;
        public int HP = 125;
        public int Bullets = 100;
        private int _hp;
        private int _bullets;
        private SpawnManager _spawnManager;
        private PlayerController _player;
        [SerializeField] private TMP_Text _bulletsCount;
        [SerializeField] private TMP_Text _healthBar;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private GameObject _bossFightPanel;

        private void OnEnable()
        {
            _player = GetComponentInParent<PlayerController>();
            CheckPlayerStats();
            _player.OnPlayerHealthValueChangedEvent += SetHP;
            _spawnManager = GameObject.Find("SpawnPointsManager").GetComponent<SpawnManager>();
            _dialogPanel = GameObject.FindWithTag("BossDialog");
            _bossFightPanel = GameObject.Find("BossFightPanel");
            _bossFightPanel.SetActive(false);

        }

        private void OnDisable()
        {
            _player.OnPlayerHealthValueChangedEvent -= SetHP;
        }
        public void SetHP(int HP)
        {

            _hp = HP;
            if (_hp > 0)
            {
                if (_healthBar != null)
                {
                    _healthBar.text = $"{_hp}";
                }
                else
                {
                    _healthBar.text = $"Current health: undefined";
                }
            }
            else
            {
                GameOver();
            }
        }

        public void SetBullets(int Bullets)
        {
            _bullets = Bullets;
            if (_bulletsCount != null)
            {
                _bulletsCount.text = $"{_bullets}";
            }
            else
            {
                _bulletsCount.text = $"undefined";
            }
        }

        public void GameOver()
        {
            if (_gameOverScreen != null)
            {
                GameIsOver = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                _gameOverScreen.SetActive(true);

            }
        }

        private void Start()
        {
            
        }

        private void CheckPlayerStats()
        {
            _healthBar = GameObject.Find("HP").GetComponent<TMP_Text>();
            _bulletsCount = GameObject.Find("Bullets").GetComponent<TMP_Text>();
            _gameOverScreen = GameObject.Find("GAMEOVER").gameObject;
            SetHP(HP);
            SetBullets(Bullets);
            if (_gameOverScreen != null)
            {
                _gameOverScreen.SetActive(false);
            }
            Time.timeScale = 1;
        }


        public void Retry()
        {
            if (_gameOverScreen != null)
            {
                _spawnManager.Resume = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void Exit()
        {
            if (_gameOverScreen != null)
            {
                
                _spawnManager.Resume = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


        public void EndDialog()
        {
            Cursor.lockState = CursorLockMode.Locked;
            transform.parent.GetComponent<PlayerMove>().IsDialog = false;
            transform.parent.GetComponent<MouseLook>().IsDialog = false;
            _dialogPanel.SetActive(false);
            FindObjectOfType<BossDialogSystem>().SittingIdle(false, false);
            _bossFightPanel.SetActive(true);
            FindObjectOfType<BossFightAI>().StartFight = true;
            
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag("BossArm"))
            {
                SetHP(50);
            }
        }

    }


}
