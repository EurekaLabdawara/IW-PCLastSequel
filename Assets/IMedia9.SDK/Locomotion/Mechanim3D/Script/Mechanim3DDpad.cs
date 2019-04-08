using UnityEngine;


namespace IMedia9
{

    public class Mechanim3DDpad : MonoBehaviour
    {
        [Header("Leftpad Settings")]
        public bool isLeftActive = true;
        public CharacterController TargetController;
        public DPadTouchLeft leftJoystick; // the game object containing the LeftJoystick script
        private Vector3 leftJoystickInput; // holds the input of the Left Joystick
        private Vector3 rightJoystickInput; // hold the input of the Right Joystick
        Quaternion targetRotation;
        [Space(10)]

        [Header("Rightpad Settings")]
        public bool isRightActive = true;
        public GameObject TargetObject;
        public DPadTouchRight rightJoystick; // the game object containing the RightJoystick script
        [Space(10)]

        [Header("Calculation Settings")]
        public bool isFlipDirection;
        public float moveSpeed = 6.0f; // movement speed of the player character
        public int rotationSpeed = 8; // rotation speed of the player character
        public float touchSensitivity = 1;
        public float moveSensitivity = 1;
        public float rotateSensitivity = 1;
        public float gravity = 20.0F;

        float targetRotationX, targetRotationY;

        private Vector3 moveDirection = Vector3.zero;
        CharacterController charController;


        private Vector2 lastPosition;

        void Start()
        {
            charController = TargetController;

            if (leftJoystick == null)
            {
                Debug.LogError("The left joystick is not attached.");
            }

            if (TargetController == null)
            {
                Debug.LogError("The target rotation game object is not attached.");
            }
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            // get input from both joysticks
            if (isFlipDirection)
            {
                leftJoystickInput = -1 * leftJoystick.GetInputDirection();
                rightJoystickInput = -1 * rightJoystick.GetInputDirection();
            }
            else { 
                leftJoystickInput = leftJoystick.GetInputDirection();
                rightJoystickInput = rightJoystick.GetInputDirection();
            }

            float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
            float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

            float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
            float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

            // if there is no input on the left joystick
            if (leftJoystickInput == Vector3.zero)
            {

            }
            // if there is no input on the right joystick
            if (rightJoystickInput == Vector3.zero)
            {

            }

            //****************************************************************************** (1) if there is only input from the left joystick
            if (leftJoystickInput != Vector3.zero)
            {

                if (isLeftActive)
                {                
                    //== LEFT MOVEMENT BEGIN
                    //TargetObject.transform.Translate(xMovementLeftJoystick * touchSensitivity, zMovementLeftJoystick * touchSensitivity, 0);

                    float tempAngle = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
                    xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                    zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

                    // calculate the player's direction based on angle

                    leftJoystickInput = new Vector3(xMovementLeftJoystick, 0, zMovementLeftJoystick);
                    leftJoystickInput = transform.TransformDirection(leftJoystickInput);
                    leftJoystickInput *= moveSpeed;

                    // rotate the player to face the direction of input
                    Vector3 temp = transform.position;
                    temp.x += xMovementLeftJoystick;
                    temp.z += zMovementLeftJoystick;
                    Vector3 lookDirection = temp - transform.position;
                    if (lookDirection != Vector3.zero)
                    {
                        TargetController.transform.localRotation = Quaternion.Slerp(TargetController.transform.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
                    }


                    moveDirection = transform.forward * moveSpeed * 100 * Time.deltaTime;
                    moveDirection.y -= gravity * Time.deltaTime;
                    charController.Move(moveDirection * Time.deltaTime);
                }


                //== LEFT MOVEMENT END
            } else
            {
                moveDirection = Vector3.zero;
                moveDirection.y -= gravity * Time.deltaTime;
                charController.Move(moveDirection * Time.deltaTime);
            }

            //****************************************************************************** (2) if there is only input from the right joystick
            if (rightJoystickInput != Vector3.zero)
            {
                if (isRightActive)
                {

                    //== RIGHT MOVEMENT BEGIN
                    //TargetObject.transform.Translate(xMovementLeftJoystick * touchSensitivity, zMovementLeftJoystick * touchSensitivity, 0);
                    float tempAngle = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
                    xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                    zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

                    // calculate the player's direction based on angle

                    rightJoystickInput = new Vector3(xMovementRightJoystick, 0, zMovementRightJoystick);
                    rightJoystickInput = transform.TransformDirection(rightJoystickInput);
                    rightJoystickInput *= moveSpeed;

                    // rotate the player to face the direction of input
                    Vector3 temp = transform.position;
                    temp.x += xMovementRightJoystick;
                    temp.z += zMovementRightJoystick;
                    Vector3 lookDirection = temp - transform.position;
                    if (lookDirection != Vector3.zero)
                    {
                        TargetObject.transform.localRotation = Quaternion.Slerp(TargetObject.transform.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
                    }

                }

             }

            //****************************************************************************** (3) if there is input from both joysticks (Left And Right)
            if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero)
            {

                /*******
                // calculate the player's direction based on angle
                float tempAngleInputRightJoystick = Mathf.Atan2(zMovementRightJoystick, xMovementRightJoystick);
                xMovementRightJoystick *= Mathf.Abs(Mathf.Cos(tempAngleInputRightJoystick));
                zMovementRightJoystick *= Mathf.Abs(Mathf.Sin(tempAngleInputRightJoystick));

                // rotate the player to face the direction of input
                Vector3 temp = transform.position;
                temp.x += xMovementRightJoystick;
                temp.z += zMovementRightJoystick;
                Vector3 lookDirection = temp - transform.position;
                if (lookDirection != Vector3.zero)
                {
                    TargetController.localRotation = Quaternion.Slerp(TargetController.localRotation, Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 45f, 0), rotationSpeed * Time.deltaTime);
                }

                // calculate the player's direction based on angle
                float tempAngleLeftJoystick = Mathf.Atan2(zMovementLeftJoystick, xMovementLeftJoystick);
                xMovementLeftJoystick *= Mathf.Abs(Mathf.Cos(tempAngleLeftJoystick));
                zMovementLeftJoystick *= Mathf.Abs(Mathf.Sin(tempAngleLeftJoystick));

                leftJoystickInput = new Vector3(xMovementLeftJoystick, 0, zMovementLeftJoystick);
                leftJoystickInput = transform.TransformDirection(leftJoystickInput);
                leftJoystickInput *= moveSpeed;

                // move the player
                transform.Translate(leftJoystickInput * Time.fixedDeltaTime);
                *******/
            }

            


        }
    }

}