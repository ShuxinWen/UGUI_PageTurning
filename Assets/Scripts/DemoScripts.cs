
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class DemoScripts : MonoBehaviour
{

    //显示条目的父物体
    private Transform ItemFather;
    //条目
    private List<GameObject> Item = new List<GameObject>();
    //当前页数
    private int page;

    // 上一页
    private Button button_Up;
    // 下一页
    private Button button_Next;
    //页码按钮
    private List<Button> pageBtn = new List<Button>();

    private readonly Color _color = new Color(0.4191176f, 0.4191176f, 0.4191176f, 1);

    /// <summary>
    /// 总条目（每条的ID）
    /// </summary>
    private int[] itemID = new int[135];


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < itemID.Length; i++)
        {
            itemID[i] = i+1;
        }

        ItemFather = GameObject.Find("Item").transform.Find("ItemFather");
        for (int i = 0; i < ItemFather.childCount; i++)
        {
            Item.Add(ItemFather.GetChild(i).gameObject);
        }
        //上一页/下一页
        button_Up = GameObject.Find("页码").transform.Find("上一页").GetComponent<Button>();
        button_Next = GameObject.Find("页码").transform.Find("下一页").GetComponent<Button>();

        button_Up.onClick.AddListener(UpPageBtn);
        button_Next.onClick.AddListener(NextPageBtn);
        //五个页码
        for (int i = 1; i <= 5; i++)
        {
            Button button = GameObject.Find("页码").transform.Find(i.ToString()).GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                ChangePageBtn(int.Parse(button.transform.GetChild(0).GetComponent<Text>().text));
            });
            pageBtn.Add(button);
        }
        ShowItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 显示成绩条
    /// </summary>
    void ShowItem()
    {
        //设置页码
        SetPage(page + 1, itemID.Length);

        for (int i = 0; i < Item.Count; i++)
        {
            Item[i].SetActive(false);
        }
        //开始索引，根据页数来
        int startIndex = 10 * page;
        int itemIndex = 0;
        for (int i = startIndex; i < startIndex + 10; i++)
        {
            if (i < itemID.Length)
            {
                //获取条目
                GameObject _item;

                if (i >= 10)
                    itemIndex = i % 10;
                else
                    itemIndex = i;
                _item = Item[itemIndex];

                //设置成绩条内容
                _item.transform.GetChild(0).GetComponent<Text>().text = itemID[i].ToString();
                //显示成绩条
                _item.SetActive(true);
            }
            else
                break;
        }
    }

    /// <summary>
    /// 上一页
    /// </summary>
    void UpPageBtn()
    {
        //当前页是第一页，返回
        if (page == 0) return;
        page -= 1;
        ShowItem();
    }
    /// <summary>
    /// 下一页
    /// </summary>
    void NextPageBtn()
    {
        if (page + 1 == (int)Mathf.Ceil((float)itemID.Length / 10f)) return;
        page += 1;
        ShowItem();
    }
    /// <summary>
    /// 点击页码切换至对应界面
    /// </summary>
    /// <param name="_page"></param>
    void ChangePageBtn(int _page)
    {
        page = _page - 1;
        ShowItem();
    }
    /// <summary>
    /// 设置页码
    /// </summary>
    /// <param name="nowPage">当前页码</param>
    /// <param name="itemCount">总成条目数</param>
    void SetPage(int nowPage, int itemCount)
    {
        //总页数
        int pageCount = (int)Mathf.Ceil((float)itemCount / 10f);
        #region 小于5页时显示页码
        if (pageCount < 5)
        {
            for (int i = 0; i < pageBtn.Count; i++)
            {
                if (i < pageCount)
                    pageBtn[i].gameObject.SetActive(true);
                else
                    pageBtn[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < pageBtn.Count; i++)
            {
                pageBtn[i].gameObject.SetActive(true);
            }
        }
        #endregion

        if (nowPage <= pageCount - 2 && nowPage >= 3)
        {
            //中间值亮
            for (int i = 0; i < pageBtn.Count; i++)
            {
                pageBtn[i].transform.GetChild(0).GetComponent<Text>().text = (nowPage + (i - 2)).ToString();
                if (i == 2)
                    pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                else
                    pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = _color;
            }
        }
        else if (nowPage <= 3)
        {
            //前三个中的一个亮
            for (int i = 0; i < pageBtn.Count; i++)
            {
                pageBtn[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                if (i + 1 == nowPage)
                    pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                else
                    pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = _color;
            }

        }
        else if (nowPage > pageCount - 2 && nowPage <= pageCount)
        {
            //后2个中的一个亮           
            if (nowPage == pageCount - 1)
            {
                for (int i = 0; i < pageBtn.Count; i++)
                {
                    if (i == 3)
                        pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    else
                        pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = _color;
                }
            }
            else if (nowPage == pageCount)
            {
                for (int i = 0; i < pageBtn.Count; i++)
                {
                    if (i == 4)
                        pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                    else
                        pageBtn[i].transform.GetChild(0).GetComponent<Text>().color = _color;
                }
            }
            for (int i = 0; i < pageBtn.Count; i++)
            {
                pageBtn[i].transform.GetChild(0).GetComponent<Text>().text = (pageCount - 4 + i).ToString();
            }
        }
    }
}
