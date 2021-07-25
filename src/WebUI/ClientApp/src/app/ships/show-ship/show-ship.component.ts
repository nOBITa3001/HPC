import { Component, OnInit } from "@angular/core";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { ShipDto, ShipsClient } from "../../web-api-client";
import { AddEditShipComponent } from "../add-edit-ship/add-edit-ship.component";

@Component({
  selector: "app-show-ship",
  templateUrl: "./show-ship.component.html",
  styleUrls: ["./show-ship.component.scss"],
})
export class ShowShipComponent implements OnInit {
  list: ShipDto[];
  ship: ShipDto;

  constructor(
    private ShipsClient: ShipsClient,
    private toastrService: NbToastrService,
    private dialogService: NbDialogService
  ) {}

  ngOnInit(): void {
    this.refreshShipList();
  }

  addClick(): void {
    this.ship = ShipDto.fromJS({
      id: 0,
      name: null,
      code: null,
      lengthInMetres: null,
      widthInMetres: null,
    });

    this.dialogService
      .open(AddEditShipComponent, {
        context: {
          title: "New ship",
          ship: this.ship,
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((_) => this.refreshShipList());
  }

  editClick(item: ShipDto): void {
    this.ship = item;

    this.dialogService
      .open(AddEditShipComponent, {
        context: {
          title: "Update ship",
          ship: this.ship,
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((_) => this.refreshShipList());
  }

  deleteClick(item: ShipDto): void {
    if (confirm("Are you sure?")) {
      this.ShipsClient.delete(item.id, "1").subscribe((res) => {
        this.showToast(
          "success",
          "top-right",
          "Success",
          `Ship ${item.name} has been deleted`
        );
        this.refreshShipList();
      });
    }
  }

  refreshShipList() {
    this.ShipsClient.get(1, 30, "1").subscribe((res) => {
      this.list = res.payload;
    });
  }

  showToast(status, position, title, message) {
    this.toastrService.show(message, title, { position, status });
  }
}
