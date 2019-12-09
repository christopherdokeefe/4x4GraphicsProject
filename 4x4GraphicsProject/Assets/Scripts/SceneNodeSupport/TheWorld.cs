using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour  {

    public SceneNode TheRoot;
    private PlayerControl player;
    private PlatformManager platformManager;
    private MainController mainController;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        Debug.Assert(player); 
        platformManager = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();
        Debug.Assert(platformManager);
        mainController = GameObject.Find("Controller").GetComponent<MainController>();
        Debug.Assert(mainController);
    }

    void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);
    }

    public void ResetWorld()
    {
        mainController.Reset();
        platformManager.Reset();
        player.Reset();
        ResetSceneNodes(TheRoot.gameObject);
    }

    private void ResetSceneNodes(GameObject currentNode)
    {
        if (currentNode.GetComponent<SceneNode>() != null)
        {
            currentNode.GetComponent<SceneNode>().ResetPosition();
            if (currentNode.transform.childCount == 0)
            {
                return;
            }
            else
            {
                int childCount = currentNode.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    ResetSceneNodes(currentNode.transform.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            return;
        }
    }
}
