using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCGKit
{
    public class Bizier_Arrow : MonoBehaviour
    {

        #region Pubic Fields
        public GameObject ArrowHead_Prefab;
        public GameObject ArrowNode_Prefab;
        public int ArrowNode_Num;
        public float Scale_Factor = 1f;
        #endregion

        #region Private Fields
        private RectTransform Origin;
        private List<RectTransform> ArrowNodes = new List<RectTransform>();
        private List<Vector2> ControlPoints = new List<Vector2>();
        private readonly List<Vector2> Control_PointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };
        #endregion

        #region Private Methods
        private void Awake()
        {
            this.Origin = this.GetComponent<RectTransform>();

            for (int i = 0; i < this.ArrowNode_Num; i++)
            {
                this.ArrowNodes.Add(Instantiate(this.ArrowNode_Prefab, this.transform).GetComponent<RectTransform>());
            }
            this.ArrowNodes.Add(Instantiate(this.ArrowHead_Prefab, this.transform).GetComponent<RectTransform>());
            this.ArrowNodes.ForEach(a => a.GetComponent<RectTransform>().position = new Vector2(-500, -500));

            for (int i = 0; i < 4; ++i)
            {
                this.ControlPoints.Add(Vector2.zero);
            }
        }

        private void Update()
        {
            this.ControlPoints[0] = new Vector2(this.Origin.position.x, this.Origin.position.y);
            this.ControlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            this.ControlPoints[1] = this.ControlPoints[0] + (this.ControlPoints[3] - this.ControlPoints[0] * this.Control_PointFactors[0]);
            this.ControlPoints[2] = this.ControlPoints[0] + (this.ControlPoints[3] - this.ControlPoints[0] + this.Control_PointFactors[1]);

            for (int i = 0; i < this.ArrowNodes.Count; ++i)
            {
                var t = Mathf.Log(1f * i / (this.ArrowNodes.Count - 1) + 1f, 2f);
                this.ArrowNodes[i].position =
                    Mathf.Pow(1 - t, 3) * this.ControlPoints[0] +
                    3 * Mathf.Pow(1 - t, 2) * t * this.ControlPoints[1] +
                    3 * (1 - t) * Mathf.Pow(t, 2) * this.ControlPoints[2] +
                    Mathf.Pow(t, 3) * this.ControlPoints[3];

                if(i>0)
                {
                    var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, this.ArrowNodes[i].position - this.ArrowNodes[i - 1].position));
                    this.ArrowNodes[i].rotation = Quaternion.Euler(euler);
                }

                var Scale = this.Scale_Factor * (1f - 0.03f * (this.ArrowNodes.Count - 1 - i));
                this.ArrowNodes[i].localScale = new Vector3(Scale, Scale, 1f);
            }

            this.ArrowNodes[0].transform.rotation = this.ArrowNodes[1].transform.rotation;
        }
        #endregion
    }
}
