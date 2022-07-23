using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ReadyPlayerMe;
using UnityEngine.Animations;

    public class LoadScenes : MonoBehaviour
    {

        public bool scriptCompleto = false;

        [SerializeField] string sceneName;
        [SerializeField] private Animation moveToPortal;

        //Variables para Cargar avatar
        [SerializeField] private string PartnerURL;
        [SerializeField] private InputField inputFieldURL;
        [SerializeField] private string avatarURL;
        [SerializeField] private GameObject parentOfAvatar;
        [SerializeField] private GameObject mainCamera;

        //Vestimenta Avatar base
        [SerializeField] private GameObject eyeLeft;
        [SerializeField] private GameObject eyeRight;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject hair;
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject outfitBottom;
        [SerializeField] private GameObject outfitFootwear;
        [SerializeField] private GameObject outfitTop;
        [SerializeField] private GameObject outfitTeeth;

        //Canvases de escenarios
        [SerializeField] private GameObject canvasPontAeri;
        [SerializeField] private GameObject canvasBar;
        [SerializeField] private GameObject canvasEscenary;
        [SerializeField] private GameObject canvasKafe;
        [SerializeField] private GameObject canvasPrimavera;

        private void Start()
        {

            if (scriptCompleto)
            {

                // cambia variable para ir a escenario y activar canvases correctos 
                if (VariablesGlobales.toPontAeri == true)
                {
                    sceneName = "3_VR_Disco1";
                    canvasPontAeri.SetActive(true);
                }
                else if (VariablesGlobales.toBar == true)
                {
                    sceneName = "4_Bar";
                    canvasBar.SetActive(true);
                }
                else if (VariablesGlobales.toEscenary == true)
                {
                    sceneName = "5_Escenary";
                    canvasEscenary.SetActive(true);
                }
                else if (VariablesGlobales.toKafe == true)
                {
                    sceneName = "6_Kafe";
                    canvasKafe.SetActive(true);
                }
                else if (VariablesGlobales.toPrimavera == true)
                {
                    sceneName = "7_Primavera";
                    canvasPrimavera.SetActive(true);
                }
                else
                {
                    sceneName = "3_VR_Disco1";
                    canvasPontAeri.SetActive(true);
                }
            }

            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }


        public void MoveToPortal()
        {
            moveToPortal.Play();
        }

        public void LoadScene()
        {

            StartCoroutine(LoadPortal());
        }

        public void LoadLobby()
        {

            SceneManager.LoadScene("Lobby_Test");
        }

        public void LoadPlaza()
        {
            VariablesGlobales.DesactivaVariables();
            SceneManager.LoadScene("1_Plaza");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (this.CompareTag("Moneda"))
            {
                //StartCoroutine(LoadPortal());
            }
        }

        IEnumerator LoadPortal()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void CrearAvatar()
        {
            Application.OpenURL(PartnerURL);
        }

        public void PasteFromClipboard()
        {
            TextEditor textEditor = new TextEditor();
            textEditor.multiline = true;
            textEditor.Paste();
            inputFieldURL.text = textEditor.text;

        }

        public void LoadAvatar()
        {
            avatarURL = inputFieldURL.text;
            AvatarLoader avatarLoader = new AvatarLoader();
            avatarLoader.LoadAvatar(avatarURL, AvatarImportedCallback, AvatarLoadedCallback);
        }

        private void AvatarImportedCallback(GameObject avatar)
        {
            // called after GLB file is downloaded and imported
            Debug.Log("Avatar Imported!");
        }

        private void AvatarLoadedCallback(GameObject avatar, AvatarMetaData metaData)
        {
            // called after avatar is prepared with components and anim controller 
            Debug.Log("Avatar Loaded!");
            //avatar.gameObject.transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
            //avatar.gameObject.transform.position = new Vector3(avatar.gameObject.transform.position.x,-1.9f, avatar.gameObject.transform.position.z);

            //avatar.transform.SetParent(parentOfAvatar.transform);
            //avatar.gameObject.transform.localRotation = Quaternion.Euler(0, mainCamera.gameObject.transform.rotation.y, 0);

            //avatar.AddComponent<LockRotation>();

            foreach (Transform child in avatar.transform)
            {
                if (child.name == "Avatar_Renderer_EyeLeft")
                {
                    eyeLeft.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    eyeLeft.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_EyeRight")
                {
                    eyeRight.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    eyeRight.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Body")
                {
                    body.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    body.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Hair")
                {
                    if (hair)
                    {
                        hair.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                        hair.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                    }
                }

                if (child.name == "Avatar_Renderer_Head")
                {
                    head.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    head.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Outfit_Bottom")
                {
                    outfitBottom.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    outfitBottom.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Outfit_Footwear")
                {
                    outfitFootwear.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    outfitFootwear.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Outfit_Top")
                {
                    outfitTop.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    outfitTop.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }

                if (child.name == "Avatar_Renderer_Teeth")
                {
                    outfitTeeth.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                    outfitTeeth.gameObject.GetComponent<Renderer>().material = child.gameObject.GetComponent<Renderer>().material;
                }


            }

            avatar.SetActive(false);

            //foreach (Transform child in avatar.transform)
            //{
            //    if (child.name == "Avatar_Renderer_EyeLeft" || child.name == "Avatar_Renderer_EyeRight"  || 
            //        child.name == "Avatar_Renderer_Head" || child.name == "Avatar_Renderer_Teeth")
            //    {
            //        child.gameObject.layer = 7;
            //    }
            //}

        }

    }

