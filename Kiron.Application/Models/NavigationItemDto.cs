﻿namespace Kiron.Application.Models;
public class NavigationItemDto
{
    public int ID { get; set; }
    public string Text { get; set; }
    public int ParentID { get; set; }
    public List<NavigationItemDto> Children { get; set; }
}
