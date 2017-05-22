using UnityEngine;
using System.Collections.Generic;


namespace Steer2D
{
    public class Firefly : SteeringBehaviour 
    {
		Transform triggerObject;
		public float FleeRadius = 1;
		Vector2 AnchorPoint = Vector2.zero;

        Vector2 TargetPoint = Vector2.zero;
        public float SlowRadius = 0.5f;
        public float StopRadius = 0.2f;
		public float roamRadius = 2f;
		public float roamTriggerDistance = 0.3f;
        public bool DrawGizmos = false;

        void Start()
        {
            agent = GetComponent<SteeringAgent>();
            agent.RegisterSteeringBehaviour(this);

            AnchorPoint = this.transform.position;
            TargetPoint = this.transform.position;

            triggerObject = GameObject.Find("Player").transform;
        }

        public override Vector2 GetVelocity()
        {
			//float distance = Vector3.Distance(transform.position, TargetPoint);

            float distance = Vector3.Distance(transform.position, (Vector3)TargetPoint);
            Vector2 desiredVelocity = (TargetPoint - (Vector2)transform.position).normalized;

			float fleeDistance = Vector3.Distance(transform.position, (Vector3)triggerObject.position);


			if (fleeDistance < FleeRadius)
			{
				AnchorPoint =  this.transform.position;
			}
			else 
			{
				if (distance < StopRadius || distance < roamTriggerDistance) {
					desiredVelocity = Vector2.zero;
					TargetPoint = new Vector2 (AnchorPoint.x + Random.Range (0f, roamRadius),
					                           AnchorPoint.y + Random.Range (0f, roamRadius));
				}
			} 

			desiredVelocity = desiredVelocity * agent.MaxVelocity;

            return desiredVelocity - agent.CurrentVelocity;
        }

        void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere((Vector3)TargetPoint, SlowRadius);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere((Vector3)TargetPoint, StopRadius);
            }
        }

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.transform == triggerObject){
				Destroy(this.gameObject);
                //GameObject.Find("Player").GetComponent<PlayerAudio>().Cheer();
                triggerObject.Find("Items/FireflyJar").GetComponent<FireflyJar>().addFly();
            } 
		}
    }
}