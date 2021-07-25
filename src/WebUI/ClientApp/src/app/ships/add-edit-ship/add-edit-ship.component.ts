import { Component, Input, OnInit } from "@angular/core";
import { NbDialogRef, NbToastrService } from "@nebular/theme";
import {
  CreateShipCommand,
  ShipDto,
  ShipsClient,
  UpdateShipCommand,
} from "../../web-api-client";

@Component({
  selector: "app-add-edit-ship",
  templateUrl: "./add-edit-ship.component.html",
  styleUrls: ["./add-edit-ship.component.scss"],
})
export class AddEditShipComponent implements OnInit {
  constructor(
    private ShipsClient: ShipsClient,
    private toastrService: NbToastrService,
    protected ref: NbDialogRef<AddEditShipComponent>
  ) {}

  @Input() title: string;
  @Input() ship: ShipDto;
  id: number;
  name: string;
  code: string;
  lengthInMetres: number;
  widthInMetres: number;

  ngOnInit(): void {
    this.id = this.ship.id;
    this.name = this.ship.name;
    this.code = this.ship.code;
    this.lengthInMetres = this.ship.lengthInMetres;
    this.widthInMetres = this.ship.widthInMetres;
  }

  addShip(): void {
    const command: CreateShipCommand = CreateShipCommand.fromJS({
      name: this.name,
      code: this.code,
      lengthInMetres: this.lengthInMetres,
      widthInMetres: this.widthInMetres,
    });
    this.ShipsClient.create("1", command).subscribe(
      (res) => {
        this.showToast(
          "success",
          "top-right",
          "Success",
          "Ship has been created",
          3000
        );
        this.close();
      },
      (error) => {
        for (const [key, value] of Object.entries(error.errors)) {
          this.showToast("danger", "top-right", `${key}`, `${value}`, 10000);
        }
      }
    );
  }

  updateShip(): void {
    const command: UpdateShipCommand = UpdateShipCommand.fromJS({
      id: this.id,
      name: this.name,
      code: this.code,
      lengthInMetres: this.lengthInMetres,
      widthInMetres: this.widthInMetres,
    });
    this.ShipsClient.update(this.id, "1", command).subscribe(
      (res) => {
        this.showToast(
          "success",
          "top-right",
          "Success",
          "Ship has been updated",
          3000
        );
        this.close();
      },
      (error) => {
        for (const [key, value] of Object.entries(error.errors)) {
          this.showToast("danger", "top-right", `${key}`, `${value}`, 10000);
        }
      }
    );
  }

  close() {
    this.ref.close();
  }

  showToast(status, position, title, message, duration) {
    this.toastrService.show(message, title, {
      position,
      status,
      duration,
    });
  }
}
