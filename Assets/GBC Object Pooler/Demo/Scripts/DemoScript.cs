using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour {

    public Text CubesText;
    public Text SpheresText;

    private void Start() {
        CubesText.text = "Cubes In Pool: " + GBCObjectPooler.Instance.ItemsInPool("Cube");
        SpheresText.text = "Spheres In Pool: " + GBCObjectPooler.Instance.ItemsInPool("Sphere");
    }

    // Use this for initialization
    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Vector2 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.collider.name.Equals("Cube")) {
                    GBCObjectPooler.Instance.ReturnToPool(hitInfo.collider.gameObject, "Cube");
                }
            }
            else {
                GameObject go = GBCObjectPooler.Instance.GetPooledObject("Cube");
                go.transform.position = new Vector3(pos.x, pos.y, Random.Range(0f, -1f));
                go.SetActive(true);
            }

            Debug.Log("Cubes Left in Pool: " + GBCObjectPooler.Instance.ItemsInPool("Cube") +
                "     Spheres Left in Pool: " + GBCObjectPooler.Instance.ItemsInPool("Sphere"));

            CubesText.text = "Cubes In Pool: " + GBCObjectPooler.Instance.ItemsInPool("Cube");
        }

        if (Input.GetMouseButtonUp(1)) {
            Vector2 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                if (hitInfo.collider.name.Equals("Sphere")) {
                    GBCObjectPooler.Instance.ReturnToPool(hitInfo.collider.gameObject, "Sphere");
                }
            }
            else {
                GameObject go = GBCObjectPooler.Instance.GetPooledObject("Sphere");
                go.transform.position = new Vector3(pos.x, pos.y, Random.Range(0f, -1f));
                go.SetActive(true);
            }

            Debug.Log("Cubes Left in Pool: " + GBCObjectPooler.Instance.ItemsInPool("Cube") +
                "     Spheres Left in Pool: " + GBCObjectPooler.Instance.ItemsInPool("Sphere"));

            SpheresText.text = "Spheres In Pool: " + GBCObjectPooler.Instance.ItemsInPool("Sphere");
        }


    }
}
