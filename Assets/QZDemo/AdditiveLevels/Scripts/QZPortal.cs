using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using Unity.XR.PXR;
using Unity.VisualScripting;

public class QZPortal : NetworkBehaviour
{
    [Scene, Tooltip("Which scene to send player from here")]
    public string destinationScene;

    [Tooltip("Where to spawn player in Destination Scene")]
    public Vector3 startPosition;

    [Tooltip("Reference to child TMP label")]
    public TextMesh label; // don't depend on TMPro. 2019 errors.

    [SyncVar(hook = nameof(OnLabelTextChanged))]
    public string labelText;

    public Renderer tpMeshRender;

    public void OnLabelTextChanged(string _, string newValue)
    {
        label.text = labelText;
    }

    public override void OnStartServer()
    {
        labelText = Path.GetFileNameWithoutExtension(destinationScene);

        // Simple Regex to insert spaces before capitals, numbers
        labelText = Regex.Replace(labelText, @"\B[A-Z0-9]+", " $0");
    }

    public override void OnStartClient()
    {
        if (label.TryGetComponent(out QZLookAtMainCamera lookAtMainCamera))
            lookAtMainCamera.enabled = true;
    }

    // Note that I have created layers called Player(6) and Portal(7) and set them
    // up in the Physics collision matrix so only Player collides with Portal.
    void OnTriggerEnter(Collider other)
    {
        // tag check in case you didn't set up the layers and matrix as noted above
        if (!other.CompareTag("Player")) return;

        // applies to host client on server and remote clients
        if (other.TryGetComponent(out QZPlayerController playerController))
            playerController.enabled = false;

        if (isServer)
            StartCoroutine(SendPlayerToNewScene(other.gameObject));

        tpMeshRender.material.color = Color.red;

        if (destinationScene.Contains("Online") || destinationScene.Contains("SubLevel1"))
        {
            EnableThrough(other.gameObject);
        }
        else
        {
            DisableThrough(other.gameObject);
        }

        // ≤‚ ‘
        //StartCoroutine(SwitchVRScene());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        tpMeshRender.material.color = Color.green;
    }

    IEnumerator SwitchVRScene()
    {
        yield return new WaitForSeconds(2.0f);

        PXR_Manager.EnableVideoSeeThrough = false;
        SceneManager.LoadScene(destinationScene);
    }

    [ServerCallback]
    IEnumerator SendPlayerToNewScene(GameObject player)
    {
        if (player.TryGetComponent(out NetworkIdentity identity))
        {
            NetworkConnectionToClient conn = identity.connectionToClient;
            if (conn == null) yield break;

            // Tell client to unload previous subscene. No custom handling for this.
            conn.Send(new SceneMessage { sceneName = gameObject.scene.path, sceneOperation = SceneOperation.UnloadAdditive, customHandling = true });

            yield return new WaitForSeconds(QZAdditiveLevelsNetworkManager.singleton.fadeInOut.GetDuration());

            NetworkServer.RemovePlayerForConnection(conn, false);

            // reposition player on server and client
            player.transform.position = startPosition;
            player.transform.LookAt(Vector3.up);

            // Move player to new subscene.
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByPath(destinationScene));

            // Tell client to load the new subscene with custom handling (see NetworkManager::OnClientChangeScene).
            conn.Send(new SceneMessage { sceneName = destinationScene, sceneOperation = SceneOperation.LoadAdditive, customHandling = true });

            NetworkServer.AddPlayerForConnection(conn, player);

            // host client would have been disabled by OnTriggerEnter above
            if (NetworkClient.localPlayer != null && NetworkClient.localPlayer.TryGetComponent(out QZPlayerController playerController))
                playerController.enabled = true;
        }
    }

    public void EnableThrough(GameObject player)
    {
        PXR_Manager.EnableVideoSeeThrough = true;

        QZPlayerScript playerScript = player.GetComponent<QZPlayerScript>();

        GameObject xrOrigin = playerScript.qzPlayerRig.gameObject;
        Camera mainCamera = xrOrigin.GetComponent<XROrigin>().Camera;
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
    }

    public void DisableThrough(GameObject player)
    {
        PXR_Manager.EnableVideoSeeThrough = false;

        QZPlayerScript playerScript = player.GetComponent<QZPlayerScript>();

        GameObject xrOrigin = playerScript.qzPlayerRig.gameObject;
        Camera mainCamera = xrOrigin.GetComponent<XROrigin>().Camera;
        mainCamera.clearFlags = CameraClearFlags.Skybox;
    }
}
