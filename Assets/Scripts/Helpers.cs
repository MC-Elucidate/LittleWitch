using UnityEngine;

public static class Helpers {

    public static GameObject FindObjectInChildren(this GameObject gameObject, string gameObjectName)
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == gameObjectName)
            {
                return item.gameObject;
            }
        }

        return null;
    }
}
