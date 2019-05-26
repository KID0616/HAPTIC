/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseStampedPublisher : Publisher<Messages.Geometry.PoseStamped>
    {
        public Transform PublishedTransform;
        public string FrameId = "Unity";

        private Messages.Geometry.PoseStamped message;

        Vector3 hitNormal;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Geometry.PoseStamped
            {
                header = new Messages.Standard.Header()
                {
                    frame_id = FrameId
                }
            };
        }

        private void UpdateMessage()
        {
            message.header.Update();
            message.pose.position = GetGeometryPoint(PublishedTransform.position.Unity2Ros());
            message.pose.orientation = GetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros());

            Publish(message);
        }

        private Messages.Geometry.Point GetGeometryPoint(Vector3 position)
        {
            Messages.Geometry.Point geometryPoint = new Messages.Geometry.Point();
            geometryPoint.x = hitNormal.x;
            geometryPoint.y = hitNormal.y;
            geometryPoint.z = hitNormal.z;
            return geometryPoint;
        }

        private Messages.Geometry.Quaternion GetGeometryQuaternion(Quaternion quaternion)
        {
            Messages.Geometry.Quaternion geometryQuaternion = new Messages.Geometry.Quaternion();
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
            return geometryQuaternion;
        }

        void OnCollisionEnter(Collision collision)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if(contact.thisCollider.name  == name){
                    hitNormal = contact.normal ;
                }
                Debug.Log(hitNormal); // ログを表示する
            }

        }
        // 当たった時に呼ばれる関数
        void OnCollisionStay(Collision collision)
        {
            ///Debug.Log("Hit"); // ログを表示する
        }
        void OnCollisionExit(Collision collision)
        {
            hitNormal.x = 0;
            hitNormal.y = 0;
            hitNormal.z = 0;
            Debug.Log("End"); // ログを表示する
        }
    }
}
