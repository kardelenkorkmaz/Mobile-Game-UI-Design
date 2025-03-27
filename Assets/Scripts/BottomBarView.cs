using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarView : MonoBehaviour
{
    [SerializeField] private RectTransform highlightObject;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private float speed;

    private int _selectedButtonIndex = -1;
    private bool _isFollowing;

    public void OnButtonSelected(int index)
    {
        if (_selectedButtonIndex == index)
        {
            animators[index].Play("Button_Unselected");
            _selectedButtonIndex = -1;
            highlightObject.gameObject.SetActive(false);
            return;
        }

        highlightObject.gameObject.SetActive(true);

        if (_selectedButtonIndex != -1)
            animators[_selectedButtonIndex].Play("Button_Unselected");

        animators[index].Play("Button_Selected");
        _selectedButtonIndex = index;
        _isFollowing = true;
    }

    void Update()
    {
        if (!_isFollowing) return;

        var targetPos = buttons[_selectedButtonIndex].transform.position;
        highlightObject.position = Vector3.MoveTowards(highlightObject.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(highlightObject.position, targetPos) < 0.1f)
            _isFollowing = false;
    }

}
