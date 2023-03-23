using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheKiwiCoder {
    public class BehaviourTreeRunner : MonoBehaviour {

        // The main behaviour tree asset
        public BehaviourTree tree;

        // Storage container object to hold game object subsystems
        public Context context;
        public Blackboard blackboard = new Blackboard();
        private Dictionary<string, BehaviourTree> _treeDic;

        // Start is called before the first frame update
        void Awake() {
            context = CreateBehaviourTreeContext();
            _treeDic = new Dictionary<string, BehaviourTree>();
        }
        
        private void OnEnable()
        {
            if (tree && !_treeDic.ContainsKey(tree.name))
            {
                var nt = tree.Clone();
                nt.name = tree.name;
                _treeDic.Add(tree.name,nt);
                tree = nt;
            }
            tree.Bind(context,blackboard);
        }

        public void ChangeTree(BehaviourTree nTree)
        {
            if (!_treeDic.ContainsKey(nTree.name))
            {
                var nt = nTree.Clone();
                nt.name = nTree.name;
                _treeDic.Add(nTree.name,nt);
                tree = nt;
            }
            else
            {
                tree = _treeDic[nTree.name];
            }
        }
        

        // Update is called once per frame
        void Update() {
            if (tree) {
                tree.Update();
            }
        }

        Context CreateBehaviourTreeContext() {
            return Context.CreateFromGameObject(gameObject);
        }

        private void OnDrawGizmosSelected() {
            if (!tree) {
                return;
            }

            BehaviourTree.Traverse(tree.rootNode, (n) => {
                if (n.drawGizmos) {
                    n.OnDrawGizmos();
                }
            });
        }
    }
}