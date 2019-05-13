import { Component, OnInit } from '@angular/core';
import { FileUploader, FileLikeObject, FileItem } from 'ng2-file-upload';
import { UploadService } from '../upload.service';

@Component({
  selector: 'app-bulk-upload',
  templateUrl: './bulk-upload.component.html',
  styleUrls: ['./bulk-upload.component.css']
})
export class BulkUploadComponent implements OnInit {

  public uploader: FileUploader = new FileUploader({});
  public hasBaseDropZoneOver: boolean = false;

  constructor(private uploadService: UploadService) { }

  getFiles(): FileLikeObject[] {
    return this.uploader.queue.map((FileItem) => {
      return FileItem.file;
    });
  }

  fileOverBase(event): void {
    this.hasBaseDropZoneOver = event;
  }

  readerFiles(reorderEvent: CustomEvent): void {
    let element = this.uploader.queue.splice(reorderEvent.detail.from, 1)[0];
    this.uploader.queue.splice(reorderEvent.detail.to, 0, element);
  }

  upload() {
    let files = this.getFiles();
    console.log(files);
    let requests = [];
    files.forEach((file) => {
      let formData: FormData = new FormData();
      formData.append('file', file.rawFile, file.name);

      requests.push(this.uploadService.upload(file))
    })
  }

  ngOnInit() {
  }

}
