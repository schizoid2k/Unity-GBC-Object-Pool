/*
 * GBC Object Pooler - Demo Scene
 * Developed by John Schumacher 2019
 * http:// gamesbycandlelight.com
 * https://github.com/schizoid2k/Unity-GBC-Object-Pool
 * @CandlelightGame (Twitter)
 * 
 * The use of the GBCObjectPooler Unity asset is free.  This is a simple, yet effective,
 * object pooler library for Unity.  I have open-sourced the development of this
 * library, and I encourage you to share your updates with the community.
 * 
 * This scene demonstrates pulling and pushing games objects to their respective pools.
*/

using UnityEngine;
using UnityEngine.UI;
using GBCObjectPooler;

public class DemoScript : MonoBehaviour {

    public Text CubesText;
    public Text SpheresText;

    private void Start() {
        CubesText.text = "Cubes In Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Cube);
        SpheresText.text = "Spheres In Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Sphere);
    }

    // Use this for initialization
    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Vector2 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.collider.name.Equals(GBCPool_DemoScene_Constants.Cube)) {
                    Pooler.Instance.ReturnToPool(hitInfo.collider.gameObject, GBCPool_DemoScene_Constants.Cube);
                }
            }
            else {
                GameObject go = Pooler.Instance.GetPooledObject(GBCPool_DemoScene_Constants.Cube);
                if (go != null) {
                    go.transform.position = new Vector3(pos.x, pos.y, Random.Range(0f, -1f));
                    go.SetActive(true);
                }
            }

            Debug.Log("Cubes Left in Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Cube) +
                "     Spheres Left in Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Sphere));

            CubesText.text = "Cubes In Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Cube);
        }

        if (Input.GetMouseButtonUp(1)) {
            Vector2 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.collider.name.Equals(GBCPool_DemoScene_Constants.Sphere)) {
                    Pooler.Instance.ReturnToPool(hitInfo.collider.gameObject, GBCPool_DemoScene_Constants.Sphere);
                }
            }
            else {
                GameObject go = Pooler.Instance.GetPooledObject(GBCPool_DemoScene_Constants.Sphere);
                if (go != null) {
                    go.transform.position = new Vector3(pos.x, pos.y, Random.Range(0f, -1f));
                    go.SetActive(true);
                }
            }

            Debug.Log("Cubes Left in Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Cube) +
                "     Spheres Left in Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Sphere));

            SpheresText.text = "Spheres In Pool: " + Pooler.Instance.ItemsInPool(GBCPool_DemoScene_Constants.Sphere);
        }
    }
}
