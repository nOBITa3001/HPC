import { Component } from "@angular/core";
import { NbMenuItem } from "@nebular/theme";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
})
export class MenuComponent {
  items: NbMenuItem[] = [
    {
      title: "Home",
      icon: "home-outline",
      link: "/",
    },
    {
      title: "Ships",
      icon: "settings-outline",
      link: "ships",
    },
  ];
}
